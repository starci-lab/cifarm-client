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
        [SerializeField] private bool useLocal = true;
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
            AuthenticateAsync();
            yield return new WaitUntil(() => authenticated);

            //healthcheck
            HealthCheckAsync();

        }

        private void SetEditorCredentials()
        {
            if (useLocal)
            {
                credentials = new Credentials()
                {
                    message = "0d9c4c9f-e52e-4b00-8aaa-1e6caaa50890",
                    publicKey = "0x2a27cc686C4c00fAbAB169733E8f0A89a3e348bA",
                    signature = "0x3570775c081781655265d6784ef77535a04e161653917e98938f9503ffee5797220a8fbd6c46e0853fb4e5d3cb608af8b3be1b7397aa4c3cbda456e115ccc7251b",
                    chainKey = "avalanche"
                };
            } else
            {
                credentials = new Credentials()
                {
                    message = "1ccd5c84-93c9-4bb9-a40a-285b4c5405e2",
                    publicKey = "0x2a27cc686C4c00fAbAB169733E8f0A89a3e348bA",
                    signature = "0x2f2d66a0502b990f4e8450d54277cf4b7297d45b2e04bed2fcfc606fc2e1e72e3b72282f3a0798e45e4b1715bf90c3660eaa2bf0780bc1a9196e6468915d39a61c",
                    chainKey = "avalanche"
                };
            }
           
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
                if (useLocal)
                {
                    client = new Client("http", "localhost", 7350, "defaultkey", UnityWebRequestAdapter.Instance)
                    {
                        Timeout = 5, //5s
                        GlobalRetryConfiguration = new RetryConfiguration(1, 0)
                    };
                } else
                {
                    var scheme = useHttps ? "https" : "http";
                    client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance)
                    {
                        Timeout = 5, //5s
                        GlobalRetryConfiguration = new RetryConfiguration(1, 0)
                    };
                }

            }
            catch (Exception e)
            {
                DLogger.Log(e.Message, "Nakama", LogColors.Gold);
            }
        }

        public async void AuthenticateAsync()
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

        public async void HealthCheckAsync()
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
