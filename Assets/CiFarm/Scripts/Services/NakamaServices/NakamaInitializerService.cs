using System;
using System.Collections;
using System.Collections.Generic;
using CiFarm.Scripts.Utilities;
using Imba.Utils;
using Nakama;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaInitializerService : ManualSingletonMono<NakamaInitializerService>
    {
        [Header("TestingConfig")]
        [SerializeField] private SupportChain testChainKey;

        [SerializeField] private int testAccountNumber;

        [Header("Nakama Config")]
        [SerializeField] private bool useLocal = true;

        [SerializeField] private bool   useHttps = true;
        [SerializeField] private string host     = "api.cifarm-server.starci.net";

        [SerializeField] private int    port      = 443;
        [SerializeField] private string serverKey = "defaultkey";

        [Header("Fake user loaded")]
        [SerializeField] [ReadOnly] private string message = "1ccd5c84-93c9-4bb9-default-285b4c5405e2";

        [SerializeField] [ReadOnly] private string publicKey = "default";
        [SerializeField] [ReadOnly] private string signature = "default";
        [SerializeField] [ReadOnly] private string chainKey  = "avalanche";

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
            if (useLocal)
            {
                credentials = new()
                {
                    message   = "1cde8eaa-739c-4bc6-ab86-f35d15eb6e6b",
                    publicKey = "0x453Ef991B58486851B7a4D477fc69Be2906Ff0FB",
                    signature =
                        "0x11edf2a629655cb05a5e30da96ff8bb49694369fe0ca350a3c271fe4fe284caf0f882918978a5a8a0ca743f5f671580ad3800a3b887b57397af5bbd519a830f71c",
                    chainKey = "avalanche",
                    network  = "testnet",
                };
            }
            else
            {
                StartCoroutine(FetchCredentialsFromApi());
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
                        Timeout                  = 5, //5s
                        GlobalRetryConfiguration = new RetryConfiguration(1, 0)
                    };
                }
                else
                {
                    var scheme = useHttps ? "https" : "http";
                    client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance)
                    {
                        Timeout                  = 5, //5s
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
                var result   = await client.RpcAsync(session, "go_healthcheck");
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

        #region Testing

        private IEnumerator FetchCredentialsFromApi()
        {
            var url = "https://api.cifarm.starci.net/api/v1/authenticator/fake-signature";
            
            using UnityWebRequest webRequest = UnityWebRequest.Post(url, new Dictionary<string, string>()
            {
                { "chainKey", testChainKey.ToString() },
                { "accountNumber", testAccountNumber.ToString() }
            });

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error fetching credentials: {webRequest.error}");
                yield break;
            }

            var json     = webRequest.downloadHandler.text;
            var response = JsonConvert.DeserializeObject<TestAccountResponse>(json);

            if (response != null && response.Data != null)
            {
                credentials = new Credentials
                {
                    message   = response.Data.Message,
                    publicKey = response.Data.PublicKey,
                    signature = response.Data.Signature,
                    chainKey  = response.Data.ChainKey,
                    network   = "testnet",
                };
            }
            else
            {
                Debug.LogError("Failed to parse API response.");
            }
        }

        [Serializable]
        public class TestAccountResponse
        {
            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public TestAccountData Data { get; set; }
        }

        [Serializable]
        public class TestAccountData
        {
            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("publicKey")]
            public string PublicKey { get; set; }

            [JsonProperty("signature")]
            public string Signature { get; set; }

            [JsonProperty("chainKey")]
            public string ChainKey { get; set; }
        }

        [Serializable]
        public enum SupportChain
        {
            avalanche,
            solana,
            aptos,
        }

        #endregion
    }
}