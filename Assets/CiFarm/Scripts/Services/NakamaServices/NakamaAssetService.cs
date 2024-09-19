using CiFarm.Scripts.Utilities;
using Imba.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
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
            LoadInventoriesAsync();
            LoadWalletAsync();
        }


        [Header("Wallets")]
        [ReadOnly]
        public int golds;
       
        [Header("Inventories")]
        [SerializeField]
        public int inventoryPage;
        [ReadOnly]
        public List<Inventory> inventories;

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