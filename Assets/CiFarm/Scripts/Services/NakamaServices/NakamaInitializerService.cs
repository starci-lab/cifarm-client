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

        [SerializeField] private int port = 443;
        [SerializeField] private string serverKey = "defaultkey";
        
        [Header("Fake user config")]
        [SerializeField] private string message = "1ccd5c84-93c9-4bb9-default-285b4c5405e2";
        [SerializeField] private string publicKey = "default";
        [SerializeField] private string signature = "default";
        [SerializeField] private string chainKey  = "avalanche";
        
        [HideInInspector]
        public Client client = null;
        [HideInInspector]
        public ISession session = null;
        [HideInInspector]
        public IApiUser user = null;

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

            credentials = useLocal
            ? new()
            //{
            //    message = "d164a56e-f33b-4d3a-94e1-c5586930159f",
            //    publicKey = "0x2a27cc686C4c00fAbAB169733E8f0A89a3e348bA",
            //    signature = "0x1ec2eff2b21e9aa70ff60f54e7298ab0ceff16271ec0ab4e7bb9a7b025ef3616221a82d8464fcc33f82eb179cbce1c14d3dab5a78ce060e0c0ba5c46aab8adc41c",
            //    chainKey = "avalanche",
            //    network = "testnet",
            //}
            {
                message = "84651c85-519b-46ec-ab16-4fc9b8a72aab",
                publicKey = "0xd0e698A8FEebE32106E607aDFe4368FD781d8822",
                signature = "0x63ebbbda7852a919eb45da72f8a98d1ded1a83237c2f2a2e02ef228d2d5391567bc6051fef89c5c49412b2feb79756035e00de30d7aafdaafd7b257fb50276d81b",
                chainKey = "avalanche",
                network = "testnet",
            }
            : new()
            {
                message   = message,
                publicKey = publicKey,
                signature = signature,
                chainKey  = chainKey,
                network   = "testnet",
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
                if (useLocal)
                {
                    client = new Client("http", "localhost", 7350, "defaultkey", UnityWebRequestAdapter.Instance)
                    {
                        Timeout = 5, //5s
                        GlobalRetryConfiguration = new RetryConfiguration(1, 0)
                    };
                }
                else
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
                { "chainKey", credentials.chainKey },
                { "network", credentials.network }
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
