using System;
using System.Collections;
using System.Collections.Generic;
using CiFarm.Scripts.Utilities;
using Codice.CM.Common;
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

        [SerializeField] private int    port = 143;
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
                message = "ae1584a3-7f77-48c4-9f1b-3a1d0f0dc58c",
                publicKey = "0x2a27cc686C4c00fAbAB169733E8f0A89a3e348bA",
                signature = "0x33c619dc61cc57405d25bc593c6e203abcc0c3a671f12e28b6e57606a846fc060fb6de512e36e6af52a6c008cd33a2955d4c5be6a538519d8f9e75ab3c43f15e1b",
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
            DLogger.Log("Creating client...", "Nakama", LogColors.Gold);
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
