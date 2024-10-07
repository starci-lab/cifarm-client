using System;
using CiFarm.Scripts.Utilities;
using Imba.Utils;
using Nakama;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CiFarm.Scripts.Services.NakamaServices.NakamaRawService;
using UnityEngine;
using UnityEngine.Events;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaUserService : ManualSingletonMono<NakamaUserService>
    {
        public UnityAction OnGoldChange;
        public UnityAction OnPlayerStatsUpdate;

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => NakamaInitializerService.Instance.authenticated);

            //load
            LoadInfoAsync();
            LoadMetadataAsync();
            LoadPlayerStatsAsync();
            LoadWalletAsync();
            LoadActivityExperiencesAsync();

            //inventory
            InitInventoriesAsync();
        }

        private async void InitInventoriesAsync()
        {
            await LoadPremiumTileNftInventoriesAsync();
            await LoadInventoriesAsync();
        }

        [Header("Info")]
        [ReadOnly]
        public string userId;

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

        [Header("ActivityExperiences")]
        [ReadOnly]
        public ActivityExperiences activityExperiences;

        [Header("Wallets")]
        [ReadOnly]
        public int golds;

        [Header("Inventories")]
        public int inventoryPage;

        [ReadOnly]
        public List<Inventory> inventories;

        public async void LoadInfoAsync()
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;
            userId   = session.UserId;
            username = session.Username;

            var account = await client.GetAccountAsync(session);
            displayName = account.User.DisplayName;
            avatarUrl   = account.User.AvatarUrl;
        }

        public async void LoadMetadataAsync()
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var objects = await client.ReadStorageObjectsAsync(session, new StorageObjectId[]
            {
                new()
                {
                    Collection = CollectionType.Config.GetStringValue(),
                    Key        = ConfigKey.Metadata.GetStringValue(),
                    UserId     = session.UserId,
                }
            });
            metadata = JsonConvert.DeserializeObject<Metadata>(objects.Objects.First().Value);
        }

        public async void LoadActivityExperiencesAsync()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var objects = await client.ReadStorageObjectsAsync(session, new StorageObjectId[]
            {
                new()
                {
                    Collection = CollectionType.Config.GetStringValue(),
                    Key        = ConfigKey.ActivityExperiences.GetStringValue(),
                    UserId     = session.UserId,
                }
            });
      
            if (objects.Objects.Any())
            {
                activityExperiences = JsonConvert.DeserializeObject<ActivityExperiences>(objects.Objects.First().Value);
            }
            else
            {
                DLogger.LogWarning("No activity experiences found for the user.");
            }
        }

        public async void LoadPlayerStatsAsync()
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var objects = await client.ReadStorageObjectsAsync(session, new StorageObjectId[]
            {
                new()
                {
                    Collection = CollectionType.Config.GetStringValue(),
                    Key        = ConfigKey.PlayerStats.GetStringValue(),
                    UserId     = session.UserId,
                }
            });
            playerStats = JsonConvert.DeserializeObject<PlayerStats>(objects.Objects.First().Value);
            OnPlayerStatsUpdate?.Invoke();
        }

        public async void LoadWalletAsync()
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var account = await client.GetAccountAsync(session);
            var wallet  = JsonConvert.DeserializeObject<Wallet>(account.Wallet);

            golds = wallet.golds;
            DLogger.Log("Wallet loaded", "Nakama - Wallet", LogColors.LimeGreen);
            OnGoldChange?.Invoke();
        }

        public async Task LoadInventoriesAsync()
        {
            var listInventoriesResponse = await NakamaRpcService.Instance.ListInventoriesRpcAsync();
            inventories = listInventoriesResponse.inventories;

            DLogger.Log("Inventories loaded", "Nakama - Inventories", LogColors.LimeGreen);
        }

        public async Task LoadPremiumTileNftInventoriesAsync()
        {
            try
            {
                var updatePremiumTileNftsRpcResponse = await NakamaRpcService.Instance.UpdatePremiumTileNftsRpcAsync();
                if (updatePremiumTileNftsRpcResponse != null && updatePremiumTileNftsRpcResponse.tokenIds != null)
                {
                    DLogger.Log($"{updatePremiumTileNftsRpcResponse.tokenIds.Count} nfts loaded",
                        "Nakama - Inventories", LogColors.LimeGreen);
                }
            }
            catch (Exception e)
            {
                DLogger.LogError("NFT LOAD FAIL: " + e.Message, "Nakama - Inventories", LogColors.LimeGreen);
            }
        }
    }
}