using Imba.Utils;
using System;
using Newtonsoft.Json;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using CiFarm.Scripts.Utilities;
using System.Collections.Generic;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaRpcService : ManualSingletonMono<NakamaRpcService>
    {
        #region Inititialized

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => NakamaInitializerService.Instance.authenticated);

            //Testing
            DoIntergrationTests();
        }

        private async void DoIntergrationTests()
        {
            //var result = await BuySeedRpcAsync(new()
            //{
            //    quantity = 1,
            //    key = "carrot"
            //});
            //DLogger.Log(JsonConvert.SerializeObject(result), LogColors.LimeGreen);
        }

        #endregion

        //Shop Rpcs

        #region BuySeedsRpc

        public class BuySeedsRpcAsyncParams
        {
            [JsonProperty("key")]
            public string key;

            [JsonProperty("quantity")]
            public int quantity;
        }

        public class BuySeedsRpcAsyncResponse
        {
            [JsonProperty("inventorySeedKey")]
            public string inventorySeedKey;
        }

        public async Task<BuySeedsRpcAsyncResponse> BuySeedsRpcAsync(
            BuySeedsRpcAsyncParams _params
        )
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "buy_seeds", JsonConvert.SerializeObject(_params));

            return JsonConvert.DeserializeObject<BuySeedsRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region ConstructBuildingRpc

        public class ConstructBuildingRpcAsyncParams
        {
            [JsonProperty("key")]
            public string key;

            [JsonProperty("position")]
            public Position position;
        }

        public class ConstructBuildingRpcAsyncResponse
        {
            [JsonProperty("cost")]
            public int cost;
        }

        public async Task<ConstructBuildingRpcAsyncResponse> ConstructBuildingRpcAsync(
            ConstructBuildingRpcAsyncParams _params
        )
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "construct_building", JsonConvert.SerializeObject(_params));

            return JsonConvert.DeserializeObject<ConstructBuildingRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region BuyAnimalRpc

        public class BuyAnimalRpcAsyncParams
        {
            [JsonProperty("key")]
            public string key;

            [JsonProperty("placedItemBuildingKey")]
            public string placedItemBuildingKey;
        }

        public class BuyAnimalRpcAsyncResponse
        {
            [JsonProperty("placedItemAnimalKey")]
            public string placedItemAnimalKey;
        }

        public async Task<BuyAnimalRpcAsyncResponse> BuyAnimalRpcAsync(
            BuyAnimalRpcAsyncParams _params
        )
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;
            var result = await client.RpcAsync(session, "buy_animal", JsonConvert.SerializeObject(_params));

            return JsonConvert.DeserializeObject<BuyAnimalRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region BuySuppliesRpc

        public class BuySuppliesRpcAsyncParams
        {
            [JsonProperty("key")]
            public string key;

            [JsonProperty("quantity")]
            public int quantity;
        }
        public class BuySuppliesRpcAsyncResponse
        {
            [JsonProperty("inventorySupplyKey")]
            public string inventorySupplyKey;
        }

        public async Task<BuySuppliesRpcAsyncResponse> BuySuppliesRpcAsync(
            BuySuppliesRpcAsyncParams _params
        )
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "buy_supplies", JsonConvert.SerializeObject(_params));

            return JsonConvert.DeserializeObject<BuySuppliesRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region BuyTileRpc

        public class BuyTileRpcAsyncParams
        {
            [JsonProperty("position")]
            public Position position;
        }
        public class BuyTileRpcAsyncResponse
        {
            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;
        }

        public async Task<BuyTileRpcAsyncResponse> BuyTileRpcAsync(
            BuyTileRpcAsyncParams _params
        )
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "buy_tile", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<BuyTileRpcAsyncResponse>(result.Payload);
        }

        #endregion

        //Farming Rpcs

        #region PlantSeedsRpc

        public class PlantSeedRpcAsyncParams
        {
            [JsonProperty("inventorySeedKey")]
            public string inventorySeedKey;

            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;
        }

        public class PlantSeedRpcAsyncResponse
        {
            [JsonProperty("seedInventoryKey")]
            public string seedInventoryKey;
        }

        public async Task<PlantSeedRpcAsyncResponse> PlantSeedRpcAsync(
            PlantSeedRpcAsyncParams _params)
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;
            var result  = await client.RpcAsync(session, "plant_seed", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<PlantSeedRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region HarvestCropRpc

        public class HarvestCropRpcAsyncParams
        {
            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;
        }

        public class HarvestCropRpcAsyncResponse
        {
            [JsonProperty("inventoryHarvestCropKey")]
            public int inventoryHarvestCropKey;
        }

        public async Task<HarvestCropRpcAsyncResponse> HarvestCropRpcAsync(
            HarvestCropRpcAsyncParams _params
        )
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "harvest_crop", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<HarvestCropRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region WaterRpc

        public class WaterRpcAsyncParams
        {
            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;
        }

        public class WaterRpcAsyncResponse
        {
        }

        public async Task<WaterRpcAsyncResponse> WaterRpcAsync(
            WaterRpcAsyncParams _params)
        {
            if (!NakamaInitializerService.Instance.authenticated) throw new Exception("Unauthenticated");
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "water", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<WaterRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region UsePestisideRpc

        public class UsePestisideRpcAsyncParams
        {
            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;
        }

        public class UsePestisideRpcAsyncResponse
        {
        }

        public async Task<UsePestisideRpcAsyncResponse> UsePestisideRpcAsync(
            UsePestisideRpcAsyncParams _params
        )
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;


            var result = await client.RpcAsync(session, "use_pestiside", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<UsePestisideRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region UseHerbicideRpc

        public class UseHerbicideRpcAsyncParams
        {
            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;
        }

        public class UseHerbicideRpcAsyncResponse
        {
        }

        public async Task<UsePestisideRpcAsyncResponse> UseHerbicideRpcAsync(
            UseHerbicideRpcAsyncParams _params
        )
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;


            var result = await client.RpcAsync(session, "use_herbicide", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<UsePestisideRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region FeedAnimalRpc
        public class FeedAnimalRpcAsyncParams
        {
            [JsonProperty("placedItemAnimalKey")]
            public string placedItemAnimalKey;

            [JsonProperty("inventoryAnimalFeedKey")]
            public string inventoryAnimalFeedKey;
        }

        public async Task FeedAnimalRpcAsync(
            FeedAnimalRpcAsyncParams _params
        )
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            await client.RpcAsync(session, "feed_animal", JsonConvert.SerializeObject(_params));
        }
        #endregion

        #region CollectAnimalProductRpc
        public class CollectAnimalProductRpcAsyncParams
        {
            [JsonProperty("placedItemAnimalKey")]
            public string placedItemAnimalKey;
        }

        public class CollectAnimalProductRpcAsyncResponse
        {
            [JsonProperty("inventoryAnimalProductKey")]
            public string inventoryAnimalProductKey;
        }

        public async Task<CollectAnimalProductRpcAsyncResponse> CollectAnimalProductAsync(
            CollectAnimalProductRpcAsyncParams _params
        )
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "collect_animal_product", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<CollectAnimalProductRpcAsyncResponse>(result.Payload);
        }
        #endregion

        //Daily Rewards Rpcs

        #region ClaimDailyRewardRpc

        public class ClaimDailyRewardRpcAsyncResponse
        {
            [JsonProperty("amount")]
            public int amount;

            [JsonProperty("days")]
            public int days;
        }

        public async Task<ClaimDailyRewardRpcAsyncResponse> ClaimDailyRequestRpcAsync()
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "claim_daily_reward");

            NakamaUserService.Instance.LoadWalletAsync();

            return JsonConvert.DeserializeObject<ClaimDailyRewardRpcAsyncResponse>(result.Payload);
        }

        #endregion

        //Assets Rpcs

        #region ListInventoriesRpc

        public class ListInventoriesRpcAsyncResponse
        {
            [JsonProperty("inventories")]
            public List<Inventory> inventories;
        }

        public async Task<ListInventoriesRpcAsyncResponse> ListInventoriesRpcAsync()
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "list_inventories");

            NakamaUserService.Instance.LoadWalletAsync();

            return JsonConvert.DeserializeObject<ListInventoriesRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region ListDeliveringProductsAsync

        public class ListDeliveringProductsRpcAsyncResponse
        {
            [JsonProperty("deliveringProducts")]
            public List<DeliveringProduct> deliveringProducts;
        }

        public async Task<ListDeliveringProductsRpcAsyncResponse> ListDeliveringProductsRpcAsync()
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "list_delivering_products");

            return JsonConvert.DeserializeObject<ListDeliveringProductsRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region DeliverProductsAsync

        public class InventoryWithIndex
        {
            [JsonProperty("index")]
            public int index;

            [JsonProperty("inventory")]
            public Inventory inventory;
        }

        public class DeliverProductsRpcAsyncParams
        {
            [JsonProperty("inventoryWithIndexes")]
            public List<InventoryWithIndex> inventoryWithIndexes;
        }

        public class DeliverProductsRpcAsyncResponse
        {
            [JsonProperty("deliveryProductKeys")]
            public List<string> deliveryProductKeys;
        }

        public async Task<DeliverProductsRpcAsyncResponse> DeliverProductsRpcAsync(
            DeliverProductsRpcAsyncParams _params)
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "deliver_products", JsonConvert.SerializeObject(_params));

            return JsonConvert.DeserializeObject<DeliverProductsRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region RetainProductsAsync

        public class RetainProductsRpcAsyncParams
        {
            [JsonProperty("deliveringProducts")]
            public List<DeliveringProduct> deliveringProducts;
        }

        public class RetainProductsRpcAsyncResponse
        {
            [JsonProperty("inventoryKeys")]
            public List<string> inventoryKeys;
        }

        public async Task<RetainProductsRpcAsyncResponse> RetainProductsRpcAsync(RetainProductsRpcAsyncParams _params)
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "retain_products", JsonConvert.SerializeObject(_params));

            return JsonConvert.DeserializeObject<RetainProductsRpcAsyncResponse>(result.Payload);
        }

        #endregion

        //Community Rpcs

        #region SearchUsersRpc

        public class SearchUsersRpcAsyncParams
        {
            [JsonProperty("value")]
            public string value;
        }

        public class SearchUsersAsyncResponse
        {
            [JsonProperty("users")]
            public List<User> users;
        }

        public async Task<SearchUsersAsyncResponse> SearchUsersRpcAsync(SearchUsersRpcAsyncParams _params)
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "search_users", JsonConvert.SerializeObject(_params));

            return JsonConvert.DeserializeObject<SearchUsersAsyncResponse>(result.Payload);
        }

        #endregion

        #region GetRandomUserRpc

        public class GetRandomUserRpcAsyncResponse
        {
            [JsonProperty("user")]
            public User user;
        }

        public async Task<GetRandomUserRpcAsyncResponse> GetRandomUserAsync()
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "get_random_user");
            return JsonConvert.DeserializeObject<GetRandomUserRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region VisitRpc

        public class VisitRpcParams
        {
            [JsonProperty("userId")]
            public string userId;
        }

        public async Task VisitRpc(VisitRpcParams _params)
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;
            await client.RpcAsync(session, "visit", JsonConvert.SerializeObject(_params));
        }

        #endregion

        #region ReturnRpc

        public async Task ReturnAsyncRpc()
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;
            await client.RpcAsync(session, "return");
        }

        #endregion

        #region HelpWaterRpc

        public class HelpWaterRpcAsyncParams
        {
            [JsonProperty("userId")]
            public string userId;

            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;
        }

        public class HelpWaterRpcAsyncResponse
        {
        }

        public async Task<HelpWaterRpcAsyncResponse> HelpWaterRpcAsync(
            HelpWaterRpcAsyncParams _params
        )
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "help_water", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<HelpWaterRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region HelpUsePestisideRpc

        public class HelpUsePestisideRpcAsyncParams
        {
            [JsonProperty("userId")]
            public string userId;

            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;
        }

        public class HelpUsePestisideRpcAsyncResponse
        {
        }

        public async Task<HelpUsePestisideRpcAsyncResponse> HelpUsePestisideRpcAsync(
            HelpUsePestisideRpcAsyncParams _params
        )
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "help_use_pestiside", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<HelpUsePestisideRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region HelpUseHerbicideRpc

        public class HelpUseHerbicideRpcAsyncParams
        {
            [JsonProperty("userId")]
            public string userId;

            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;
        }

        public class HelpUseHerbicideRpcAsyncResponse
        {
        }

        public async Task<HelpUseHerbicideRpcAsyncResponse> HelpUseHerbicideRpcAsync(
            HelpUseHerbicideRpcAsyncParams _params
        )
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "help_use_herbicide", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<HelpUseHerbicideRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region ThiefCropRpc

        public class ThiefCropRpcAsyncParams
        {
            [JsonProperty("userId")]
            public string userId;

            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;
        }

        public class ThiefCropRpcAsyncResponse
        {
            [JsonProperty("thiefPlantInventoryKey")]
            public string thiefPlantInventoryKey;

            [JsonProperty("thiefQuantity")]
            public int thiefQuantity;
        }

        public async Task<ThiefCropRpcAsyncResponse> ThiefCropRpcAsync(
            ThiefCropRpcAsyncParams _params
        )
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "thief_crop", JsonConvert.SerializeObject(_params));

            return JsonConvert.DeserializeObject<ThiefCropRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region HelpFeedAnimalRpc
        public class HelpFeedAnimalRpcAsyncParams
        {
            [JsonProperty("placedItemAnimalKey")]
            public string placedItemAnimalKey;

            [JsonProperty("inventoryAnimalFeedKey")]
            public string inventoryAnimalFeedKey;

            [JsonProperty("userId")]
            public string userId;
        }

        public async Task HelpFeedAnimalRpcAsync(
            HelpFeedAnimalRpcAsyncParams _params
        )
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            await client.RpcAsync(session, "help_feed_animal", JsonConvert.SerializeObject(_params));
        }
        #endregion

        #region ThiefAnimalProductRpc
        public class ThiefAnimalProductRpcAsyncParams
        {
            [JsonProperty("userId")]
            public string userId;

            [JsonProperty("placedItemAnimalKey")]
            public string placedItemAnimalKey;
        }
        public class ThiefAnimalProductRpcAsyncResponse
        {
            [JsonProperty("inventoryThiefCropKey")]
            public string inventoryThiefCropKey;

            [JsonProperty("thiefQuantity")]
            public int thiefQuantity;
        }

        public async Task<ThiefAnimalProductRpcAsyncResponse> ThiefAnimalProductRpcAsync(
            ThiefAnimalProductRpcAsyncParams _params
        )
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "thief_animal_product", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<ThiefAnimalProductRpcAsyncResponse>(result.Payload);
        }
        #endregion


        //Nft Rpcs

        #region UpdatePremiumTileNftsRpc

        public class UpdatePremiumTileNftsRpcAsyncResponse
        {
            [JsonProperty("tokenIds")]
            public List<int> tokenIds;
        }

        public async Task<UpdatePremiumTileNftsRpcAsyncResponse> UpdatePremiumTileNftsRpcAsync()
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            try
            {
                var result = await client.RpcAsync(session, "update_premium_tile_nfts");
                return JsonConvert.DeserializeObject<UpdatePremiumTileNftsRpcAsyncResponse>(result.Payload);
            }
            catch (Exception e)
            {
                DLogger.LogError("NFT LOAD FAIL: " + e.Message, "Nakama - Inventories", LogColors.LimeGreen);
                return null;
            }

        }

        #endregion

        //Miscellaneous Rpcs

        #region ForceCentralBroadcastInstantlyRpc

        public async Task ForceCentralBroadcastInstantlyRpcAsync()
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;
            await client.RpcAsync(session, "force_central_broadcast_instantly");
        }

        #endregion

        //Placement Rpcs

        #region PlaceTile

        public class PlaceTileRpcAsyncParams
        {
            [JsonProperty("inventoryTileKey")]
            public string inventoryTileKey;

            [JsonProperty("position")]
            public Position position;
        }

        public class PlaceTileRpcAsyncResponse
        {
            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;
        }

        public async Task<PlaceTileRpcAsyncResponse> PlaceTileRpcAsync(PlaceTileRpcAsyncParams _params)
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "place_tile", JsonConvert.SerializeObject(_params));

            return JsonConvert.DeserializeObject<PlaceTileRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region ConstructBuilding

        public class ConstructBuildingRpcParams
        {
            [JsonProperty("key")]
            public string Key { get; set; }

            [JsonProperty("position")]
            public Position Position { get; set; }
        }

        public class ConstructBuildingRpcResponse
        {
            [JsonProperty("buildingKey")]
            public string BuildingKey { get; set; }
        }

        public async Task<ConstructBuildingRpcResponse> ConstructBuildingRpcAsync(ConstructBuildingRpcParams _params)
        {
            var client  = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "construct_building", JsonConvert.SerializeObject(_params));

            return JsonConvert.DeserializeObject<ConstructBuildingRpcResponse>(result.Payload);
        }

        #endregion

        #region RecoverTileRpc

        public class RecoverTileRpcAsyncParams
        {
            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;

            [JsonProperty("position")]
            public Position position;
        }

        public class RecoverTileRpcAsyncResponse
        {
            [JsonProperty("inventoryTileKey")]
            public string inventoryTileKey;
        }

        public async Task<RecoverTileRpcAsyncResponse> RecoverTileRpcAsync(PlaceTileRpcAsyncParams _params)
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "recover_tile", JsonConvert.SerializeObject(_params));

            return JsonConvert.DeserializeObject<RecoverTileRpcAsyncResponse>(result.Payload);
        }

        #endregion

        #region MoveRpc

        public class MoveRpcAsyncParams
        {
            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;

            [JsonProperty("position")]
            public Position position;
        }


        public async Task MoveRpcAsync(MoveRpcAsyncParams _params)
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "move", JsonConvert.SerializeObject(_params));
        }

        #endregion

        //Profiles Rpcs

        #region UpdateTutorialRpc
        public class UpdateTutorialRpcAsyncParams
        {
            [JsonProperty("tutorialIndex")]
            public int tutorialIndex;

            [JsonProperty("stepIndex")]
            public int stepIndex;
        }

        public async Task UpdateTutorialRpcAsync(
            UpdateTutorialRpcAsyncParams _params
        )
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            await client.RpcAsync(session, "update_tutorial", JsonConvert.SerializeObject(_params));
        }
        #endregion
    }
}