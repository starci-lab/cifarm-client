using CiFarm.Scripts.Utilities;
using Codice.CM.Common;
using Imba.Utils;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaAssetService : ManualSingletonMono<NakamaAssetService>
    {
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
        }

        public async void LoadInventoriesAsync()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var objects = await client.ListUsersStorageObjectsAsync(session, CollectionType.Inventories.GetStringValue(), session.UserId, 25);
            inventories = objects.Objects.Select(_object =>
            {
                var inventory = JsonConvert.DeserializeObject<Inventory>(_object.Value);
                inventory.key = _object.Key;
                return inventory;
            }).ToList();
            DLogger.Log("Inventories loaded", "Nakama - Inventories", LogColors.LimeGreen);
        }
    }
}