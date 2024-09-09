using System;
using System.Collections.Generic;
using Imba.Utils;
using Nakama;
using Newtonsoft.Json;
using SupernovaDriver.Scripts.Utilities;
using UnityEngine;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaService : ManualSingletonMono<NakamaService>
    {
        [Header("Nakama Config")]
        [SerializeField] private string hostCf = "api.cifarm-server.starci.net";

        [SerializeField] private int    portCf = 143;
        [SerializeField] private string svKey  = "defaultkey";

        [Header("Test Parameter")]
        [SerializeField] private string message = "defaultkey";

        [SerializeField] private string publicKey = "defaultkey";
        [SerializeField] private string signature = "defaultkey";
        [SerializeField] private string chain     = "defaultkey";

        private Client   client  = null;
        private ISession session = null;
        private ISocket  socket  = null;
        private IApiUser user    = null;

        public override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            DLogger.Log("Creating client...", "Nakama", LogColors.Gold);
            InitializeClient(hostCf, portCf, svKey);
            HealthCheck();
            DLogger.Log("Logging...", "Nakama", LogColors.Gold);
            LoginWithFakeWallet();
        }

        public void InitializeClient(string host, int port, string severKey)
        {
            try
            {
                client = new Client("https", host, port, severKey, UnityWebRequestAdapter.Instance)
                {
                    Timeout                  = 8,
                    GlobalRetryConfiguration = new RetryConfiguration(1, 0)
                };
            }
            catch (Exception e)
            {
                DLogger.Log(e.Message, "Nakama", LogColors.Gold);
            }
        }

        public async void HealthCheck()
        {
            session = await client.AuthenticateDeviceAsync(SystemInfo.deviceUniqueIdentifier);
            try
            {
                var hentaizNet = await client.RpcAsync(session, "go_healthcheck");
                Debug.Log(JsonConvert.SerializeObject(hentaizNet.Payload));
            }
            catch (Exception e)
            {
                DLogger.Log(e.ToString(), "Nakama", LogColors.Gold);
            }

            DLogger.Log("healthchecked", "Nakama", LogColors.Gold);
        }

        public async void LoginWithFakeWallet()
        {
            try
            {
                session = await client.AuthenticateCustomAsync("xnxx", "hentaiz", false,
                    new Dictionary<string, string>()
                    {
                        { "message", message },
                        { "publicKey", publicKey },
                        { "signature", signature },
                        { "chain", chain },
                    }
                );

                DLogger.Log("Storage...", "Nakama", LogColors.Gold);
                StorageCheck();
            }
            catch (Exception e)
            {
                DLogger.Log(e.ToString(), "Nakama", LogColors.Gold);
            }

            DLogger.Log("Login success", "Nakama", LogColors.Gold);
        }

        public async void StorageCheck()
        {
            try
            {
                StorageObjectId personalStorageId = new StorageObjectId();
                personalStorageId.Collection = "entities";
                personalStorageId.Key        = "plants";
                IApiStorageObjects personalStorageObjects =
                    await client.ReadStorageObjectsAsync(session, new[] { personalStorageId });

                Debug.Log(JsonConvert.SerializeObject(personalStorageObjects.Objects));
            }
            catch (Exception e)
            {
                DLogger.Log(e.ToString(), "Nakama", LogColors.Gold);
            }

            DLogger.Log("Login success", "Nakama", LogColors.Gold);
        }

        public void Test()
        {
            // var plants = await client.ReadStorageObjectsAsync(client.s)
        }
    }
}