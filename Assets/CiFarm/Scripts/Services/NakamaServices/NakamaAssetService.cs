using CiFarm.Scripts.Utilities;
using Imba.Utils;
using Nakama;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaAssetService : ManualSingletonMono<NakamaAssetService>
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
            LoadPlayerStatsAsync();
            LoadInventoriesAsync();
            LoadWalletAsync();
        }

        [Header("Level")]
        [ReadOnly]
        public PlayerStats playerStats;

        [Header("Wallets")]
        [ReadOnly]
        public int golds;
       
        [Header("Inventories")]
        public int inventoryPage;
        [ReadOnly]
        public List<Inventory> inventories;


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
            var updatePremiumTileNftsRpcResponse = await NakamaRpcService.Instance.UpdatePremiumTileNftsRpcAsync();
            if (updatePremiumTileNftsRpcResponse.tokenIds != null)
            {
                DLogger.Log($"{updatePremiumTileNftsRpcResponse.tokenIds.Count} nfts loaded", "Nakama - Inventories", LogColors.LimeGreen);
            }
            
            var listInventoriesResponse = await NakamaRpcService.Instance.ListInventoriesRpcAsync();
            inventories = listInventoriesResponse.inventories;

            DLogger.Log("Inventories loaded", "Nakama - Inventories", LogColors.LimeGreen);
        }
    }
}