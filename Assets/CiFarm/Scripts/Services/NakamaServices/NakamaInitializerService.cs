using System;
using System.Collections;
using System.Collections.Generic;
using CiFarm.Scripts.Utilities;
using Imba.Utils;
using Nakama;
using Newtonsoft.Json;
using UnityEngine;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaInitializerService : ManualSingletonMono<NakamaInitializerService>
    {
        [Header("Nakama Config")]
        [SerializeField] private bool useHttps = true;
        [SerializeField] private string host = "api.cifarm-server.starci.net";

        [SerializeField] private int    port = 443;
        [SerializeField] private string serverKey  = "defaultkey";

        [HideInInspector]
        public Client   client  = null;
        [HideInInspector]
        public ISession session = null;
        [HideInInspector]
        public IApiUser user    = null;

        //credentials
        private Credentials credentials = null;

        //authenticate state
        [HideInInspector]
        public bool authenticated = false;

        public override void Awake()
        {
            base.Awake();
        }


        private IEnumerator Start()
        {
#if UNITY_EDITOR
            //set fake credentials for edtior only
            SetEditorCredentials();
#endif
            yield return new WaitUntil(() => credentials != null);
            //initialize
            InitializeClient();

            //authenticate
            Authenticate();
            yield return new WaitUntil(() => authenticated);

            //healthcheck
            HealthCheck();

        }

        private void SetEditorCredentials()
        {
            credentials = new Credentials()
            {
                message = "e224d0d8-ce9a-41c9-a8fc-5ddd03bbf69f",
                publicKey = "0x2a27cc686C4c00fAbAB169733E8f0A89a3e348bA",
                signature = "0xd239fecc5a67afd0352a089331b005ae9965b4a47001dd87fd619346410a2a44763bff159fe40eb4d1b8788f0a076318faca6ebabfab9976e206caf0bb18476b1b",
                chainKey = "avalanche"
            };
        }

        //called from React app to set credentials, then break the coroutine
        public void SetCredentials(string payload)
        {
            credentials = JsonConvert.DeserializeObject<Credentials>(payload);
        }
    
        public void InitializeClient()
        {
            try
            {   
                var scheme = useHttps ? "https" : "http";
                client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance)
                {
                    Timeout                  = 5, //5s
                    GlobalRetryConfiguration = new RetryConfiguration(1, 0)
                };
            }
            catch (Exception e)
            {
                DLogger.Log(e.Message, "Nakama", LogColors.Gold);
            }
        }

        public async void Authenticate()
        {
            try
            {
                DLogger.Log("Authenticating...", "Nakama", LogColors.Gold);
                session = await client.AuthenticateCustomAsync("starci", null, false, new Dictionary<string, string>
            {
                { "message", credentials.message },
                { "publicKey", credentials.publicKey },
                { "signature", credentials.signature },
                { "chainKey", credentials.chainKey }
            });
                authenticated = true;
            }
            catch (Exception e)
            {
                DLogger.Log(e.Message, "Nakama", LogColors.OrangeRed);
            }
        }

        public async void HealthCheck()
        {
            try
            {
                var result = await client.RpcAsync(session, "go_healthcheck");
                var response = JsonConvert.DeserializeObject<GoHealthcheckResponse>(result.Payload);
              
                if (response.status == "ok")
                {
                    DLogger.Log("Healthcheck succeeded", "Nakama", LogColors.LimeGreen);
                }
                
            }
            catch (Exception e)
            {
                DLogger.Log(e.ToString(), "Nakama", LogColors.Gold);
            }
        }
    }
}
