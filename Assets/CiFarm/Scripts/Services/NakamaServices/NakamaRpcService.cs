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
        public override void Awake()
        {
            base.Awake();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => NakamaInitializerService.Instance.authenticated);

            //Testing
            DoIntergrationTests();
        }

        private async void DoIntergrationTests ()
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
        #region BuySeedRpc
        public class BuySeedRpcAsyncParams
        {
            [JsonProperty("key")]
            public string key;
            [JsonProperty("quantity")]
            public int quantity;
        }

        public class BuySeedRpcAsyncResponse
        {
            [JsonProperty("inventorySeedKey")]
            public string inventorySeedKey;
        }
        public async Task<BuySeedRpcAsyncResponse> BuySeedRpcAsync(
            BuySeedRpcAsyncParams _params
            )
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "buy_seed", JsonConvert.SerializeObject(_params));

            NakamaAssetService.Instance.LoadWalletAsync();
            NakamaAssetService.Instance.LoadInventoriesAsync();

            return JsonConvert.DeserializeObject<BuySeedRpcAsyncResponse>(result.Payload);
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
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "construct_building", JsonConvert.SerializeObject(_params));

            NakamaAssetService.Instance.LoadWalletAsync();

            return JsonConvert.DeserializeObject<ConstructBuildingRpcAsyncResponse>(result.Payload);
        }
        #endregion
        #region BuyAnimalRpc
        public class BuyAnimalRpcAsyncParams
        {
            [JsonProperty("key")]
            public string key;
        }

        public class BuyAnimalRpcAsyncResponse
        {
            [JsonProperty("cost")]
            public int cost;
        }
        public async Task<BuyAnimalRpcAsyncResponse> BuyAnimalRpcAsync(
            BuyAnimalRpcAsyncParams _params
            )
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "buy_animal", JsonConvert.SerializeObject(_params));
            NakamaAssetService.Instance.LoadWalletAsync();
            NakamaAssetService.Instance.LoadInventoriesAsync();
            return JsonConvert.DeserializeObject<BuyAnimalRpcAsyncResponse>(result.Payload);
        }
        #endregion

        //Farming Rpcs
        #region PlantSeedRpc
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
            PlantSeedRpcAsyncParams _params
            )
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

           

            var result = await client.RpcAsync(session, "plant_seed", JsonConvert.SerializeObject(_params));
            NakamaAssetService.Instance.LoadInventoriesAsync();
            return JsonConvert.DeserializeObject<PlantSeedRpcAsyncResponse>(result.Payload);
        }
        #endregion
        #region HarvestPlantRpc
        public class HarvestPlantRpcAsyncParams
        {
            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;
        }

        public class HarvestPlantRpcAsyncResponse
        {
            [JsonProperty("harvestPlantInventoryKey")]
            public int harvestPlantInventoryKey;
        }
        public async Task<HarvestPlantRpcAsyncParams> HarvestPlantRpcAsync(
            HarvestPlantRpcAsyncParams _params
            )
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "harvest_plant", JsonConvert.SerializeObject(_params));
            NakamaAssetService.Instance.LoadInventoriesAsync();
            return JsonConvert.DeserializeObject<HarvestPlantRpcAsyncParams>(result.Payload);
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
            WaterRpcAsyncParams _params
            )
        {
            if (!NakamaInitializerService.Instance.authenticated) throw new Exception("Unauthenticated");
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;



            var result = await client.RpcAsync(session, "water", JsonConvert.SerializeObject(_params));
            NakamaAssetService.Instance.LoadInventoriesAsync();
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
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;



            var result = await client.RpcAsync(session, "use_pestiside", JsonConvert.SerializeObject(_params));
            NakamaAssetService.Instance.LoadInventoriesAsync();
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
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;



            var result = await client.RpcAsync(session, "use_herbicide", JsonConvert.SerializeObject(_params));
            NakamaAssetService.Instance.LoadInventoriesAsync();
            return JsonConvert.DeserializeObject<UsePestisideRpcAsyncResponse>(result.Payload);
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
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "claim_daily_reward");

            NakamaAssetService.Instance.LoadWalletAsync();

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
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "list_inventories");

            NakamaAssetService.Instance.LoadWalletAsync();

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
            var client = NakamaInitializerService.Instance.client;
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
       
        public async Task<DeliverProductsRpcAsyncResponse> DeliverProductsRpcAsync(DeliverProductsRpcAsyncParams _params)
        {
            var client = NakamaInitializerService.Instance.client;
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
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "retain_products", JsonConvert.SerializeObject(_params));

            return JsonConvert.DeserializeObject<RetainProductsRpcAsyncResponse>(result.Payload);
        }
        #endregion

        //Users Rpcs
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
            var client = NakamaInitializerService.Instance.client;
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
            var client = NakamaInitializerService.Instance.client;
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
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;
            await client.RpcAsync(session, "visit", JsonConvert.SerializeObject(_params));
        }
        #endregion
        #region ReturnRpc
        public async Task ReturnRpc()
        {
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;
            await client.RpcAsync(session, "return");
        }
        #endregion
    }
}   