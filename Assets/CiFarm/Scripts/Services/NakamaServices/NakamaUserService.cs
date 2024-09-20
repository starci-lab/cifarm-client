using System;
using CiFarm.Scripts.Utilities;
using Imba.Utils;
using Nakama;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaUserService : ManualSingletonMono<NakamaUserService>
    {
        public UnityAction onGoldChange;
        public override void Awake()
        {
            base.Awake();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => NakamaInitializerService.Instance.authenticated);

            //load
            LoadInfoAsync();
            LoadMetadataAsync();
            LoadPlayerStatsAsync();
            LoadInventoriesAsync();
            LoadWalletAsync();
        }

        [Header("Info")]
        [ReadOnly]
        public string username;
        [ReadOnly]
        public string displayName;
        [ReadOnly]
        public string avatarUrl;

        [Header("Metadata")]
        [ReadOnly]
        public Metadata metadata;

        [Header("Player Status")]
        [ReadOnly]
        public PlayerStats playerStats;

        [Header("Wallets")]
        [ReadOnly]
        public int golds;
       
        [Header("Inventories")]
        public int inventoryPage;
        [ReadOnly]
        public List<Inventory> inventories;

        public async void LoadInfoAsync()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;
            username = session.Username;

            var account = await client.GetAccountAsync(session);
            displayName = account.User.DisplayName;
            avatarUrl = account.User.AvatarUrl;
        }

        public async void LoadMetadataAsync()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var objects = await client.ReadStorageObjectsAsync(session, new StorageObjectId[]
            {
                new ()
                {
                    Collection = CollectionType.Config.GetStringValue(),
                    Key = ConfigKey.Metadata.GetStringValue(),
                    UserId = session.UserId,
                }
            });
            metadata = JsonConvert.DeserializeObject<Metadata>(objects.Objects.First().Value);
        }


        public async void LoadPlayerStatsAsync()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var objects = await client.ReadStorageObjectsAsync(session, new StorageObjectId[]
            {
                new ()
                {
                    Collection = CollectionType.Config.GetStringValue(),
                    Key = ConfigKey.PlayerStats.GetStringValue(),
                    UserId = session.UserId,
                }
            });
            playerStats = JsonConvert.DeserializeObject<PlayerStats>(objects.Objects.First().Value); 
        }

        public async void LoadWalletAsync()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var account = await client.GetAccountAsync(session);
            var wallet = JsonConvert.DeserializeObject<Wallet>(account.Wallet);

            golds = wallet.golds;
            DLogger.Log("Wallet loaded", "Nakama - Wallet", LogColors.LimeGreen);
            onGoldChange?.Invoke();
        }

        public async void LoadInventoriesAsync()
        {
            try
            {
                var updatePremiumTileNftsRpcResponse = await NakamaRpcService.Instance.UpdatePremiumTileNftsRpcAsync();
                if (updatePremiumTileNftsRpcResponse != null && updatePremiumTileNftsRpcResponse.tokenIds != null)
                {
                    DLogger.Log($"{updatePremiumTileNftsRpcResponse.tokenIds.Count} nfts loaded", "Nakama - Inventories", LogColors.LimeGreen);
                }
            }
            catch (Exception e)
            {
                DLogger.LogError("NFT LOAD FAIL: " + e.Message, "Nakama - Inventories", LogColors.LimeGreen);
            }
          
            
            var listInventoriesResponse = await NakamaRpcService.Instance.ListInventoriesRpcAsync();
            inventories = listInventoriesResponse.inventories;

            DLogger.Log("Inventories loaded", "Nakama - Inventories", LogColors.LimeGreen);
        }
    }
}