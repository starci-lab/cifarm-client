using Imba.Utils;
using System;
using Newtonsoft.Json;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using CiFarm.Scripts.Utilities;

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
            //var result = await BuySeedRpc(new()
            //{
            //    quantity = 1,
            //    key = "carrot"
            //});
            //DLogger.Log(JsonConvert.SerializeObject(result), LogColors.LimeGreen);
        }
        #endregion

        //Shop Rpcs
        #region BuySeedRpc
        public class BuySeedRpcParams
        {
            [JsonProperty("key")]
            public string key;
            [JsonProperty("quantity")]
            public int quantity;
        }

        public class BuySeedRpcResponse
        {
            [JsonProperty("totalCost")]
            public int totalCost;
        }
        public async Task<BuySeedRpcResponse> BuySeedRpc(
            BuySeedRpcParams _params
            )
        {
            if (!NakamaInitializerService.Instance.authenticated) throw new Exception("Unauthenticated");
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "buy_seed", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<BuySeedRpcResponse>(result.Payload);
        }
        #endregion
        #region ConstructBuildingRpc
        public class ConstructBuildingRpcParams
        {
            [JsonProperty("key")]
            public string key;
            [JsonProperty("position")]
            public Position position;
        }

        public class ConstructBuildingRpcResponse
        {
            [JsonProperty("cost")]
            public int cost;
        }
        public async Task<ConstructBuildingRpcResponse> ConstructBuildingRpc(
            ConstructBuildingRpcParams _params
            )
        {
            if (!NakamaInitializerService.Instance.authenticated) throw new Exception("Unauthenticated");
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "construct_building", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<ConstructBuildingRpcResponse>(result.Payload);
        }
        #endregion
        #region BuyAnimalRpc
        public class BuyAnimalRpcParams
        {
            [JsonProperty("key")]
            public string key;
        }

        public class BuyAnimalRpcResponse
        {
            [JsonProperty("cost")]
            public int cost;
        }
        public async Task<BuyAnimalRpcResponse> BuyAnimalRpc(
            BuyAnimalRpcParams _params
            )
        {
            if (!NakamaInitializerService.Instance.authenticated) throw new Exception("Unauthenticated");
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "buy_animal", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<BuyAnimalRpcResponse>(result.Payload);
        }
        #endregion

        //Farming Rpcs
        #region PlantSeedRpc
        public class PlantSeedRpcParams
        {
            [JsonProperty("inventorySeedKey")]
            public string inventorySeedKey;

            [JsonProperty("placedItemTileKey")]
            public string placedItemTileKey;
        }

        public class PlantSeedRpcResponse
        {
            [JsonProperty("haverstIn")]
            public int haverstIn;
        }
        public async Task<PlantSeedRpcResponse> PlantSeedRpc(
            PlantSeedRpcParams _params
            )
        {
            if (!NakamaInitializerService.Instance.authenticated) throw new Exception("Unauthenticated");
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "plant_seed", JsonConvert.SerializeObject(_params));
            return JsonConvert.DeserializeObject<PlantSeedRpcResponse>(result.Payload);
        }
        #endregion

        //Daily Rewards Rpc
        #region ClaimDailyRewardRpc
        public class ClaimDailyRewardRpcResponse
        {
            [JsonProperty("amount")]
            public int amount;
            [JsonProperty("days")]
            public int days;
        }

        public async Task<ClaimDailyRewardRpcResponse> ClaimDailyRequestRpc()
        {
            if (!NakamaInitializerService.Instance.authenticated) throw new Exception("Unauthenticated");
            var client = NakamaInitializerService.Instance.client;
            var session = NakamaInitializerService.Instance.session;

            var result = await client.RpcAsync(session, "claim_daily_reward");
            return JsonConvert.DeserializeObject<ClaimDailyRewardRpcResponse>(result.Payload);
        }
        #endregion
    }
}