using System.Linq;
using System.Threading.Tasks;
using Imba.Utils;

namespace CiFarm.Scripts.Services.NakamaServices
{
    public class NakamaFarmingService : ManualSingletonMono<NakamaFarmingService>
    {
        #region Planting

        /// <summary>
        /// Plants a seed using the provided parameters.
        /// </summary>
        /// <param name="inventorySeedKey">The key of the seed in the inventory.</param>
        /// <param name="placedItemTileKey">The tile key where the seed is planted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task PlantSeedAsync(string inventorySeedKey, string placedItemTileKey)
        {
            var paramsObj = new NakamaRpcService.PlantSeedRpcAsyncParams
            {
                inventorySeedKey  = inventorySeedKey,
                placedItemTileKey = placedItemTileKey
            };

            var response = await NakamaRpcService.Instance.PlantSeedRpcAsync(paramsObj);

            var seed = NakamaUserService.Instance.inventories.FirstOrDefault(o => o.key == inventorySeedKey);

            if (seed != null)
            {
                if (seed.quantity > 1)
                {
                    seed.quantity--;
                }
                else
                {
                    NakamaUserService.Instance.inventories.Remove(seed);
                }
            }

            await NakamaRpcService.Instance.ForceCentralBroadcastInstantlyRpcAsync();
            NakamaUserService.Instance.LoadInventoriesAsync();
            // Additional logic can be added here if needed
        }

        #endregion

        #region Harvesting

        /// <summary>
        /// Harvests a crop from the specified tile.
        /// </summary>
        /// <param name="placedItemTileKey">The tile key of the crop to harvest.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task HarvestCropAsync(string placedItemTileKey)
        {
            var paramsObj = new NakamaRpcService.HarvestCropRpcAsyncParams
            {
                placedItemTileKey = placedItemTileKey
            };

            var response = await NakamaRpcService.Instance.HarvestCropRpcAsync(paramsObj);
            NakamaUserService.Instance.LoadInventoriesAsync();
            // Additional logic can be added here if needed
        }

        #endregion

        #region Watering

        /// <summary>
        /// Waters a planted seed on the specified tile.
        /// </summary>
        /// <param name="placedItemTileKey">The tile key of the seed to water.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task WaterAsync(string placedItemTileKey)
        {
            var paramsObj = new NakamaRpcService.WaterRpcAsyncParams
            {
                placedItemTileKey = placedItemTileKey
            };

            await NakamaRpcService.Instance.WaterRpcAsync(paramsObj);
            await NakamaSocketService.Instance.ForceCentralBroadcastInstantlyRpcAsync();
        }

        #endregion

        #region Using Pesticide

        /// <summary>
        /// Uses pesticide on the specified tile.
        /// </summary>
        /// <param name="placedItemTileKey">The tile key where pesticide is applied.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UsePesticideAsync(string placedItemTileKey)
        {
            var paramsObj = new NakamaRpcService.UsePestisideRpcAsyncParams
            {
                placedItemTileKey = placedItemTileKey
            };

            await NakamaRpcService.Instance.UsePestisideRpcAsync(paramsObj);
            await NakamaSocketService.Instance.ForceCentralBroadcastInstantlyRpcAsync();
        }

        #endregion

        #region Using Herbicide

        /// <summary>
        /// Uses herbicide on the specified tile.
        /// </summary>
        /// <param name="placedItemTileKey">The tile key where herbicide is applied.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UseHerbicideAsync(string placedItemTileKey)
        {
            var paramsObj = new NakamaRpcService.UseHerbicideRpcAsyncParams
            {
                placedItemTileKey = placedItemTileKey
            };

            await NakamaRpcService.Instance.UseHerbicideRpcAsync(paramsObj);
            await NakamaSocketService.Instance.ForceCentralBroadcastInstantlyRpcAsync();
            // Additional logic can be added here if needed
        }

        #endregion

        #region Thieving Crops

        /// <summary>
        /// Thieves crops from another user.
        /// </summary>
        /// <param name="userId">The ID of the user from whom to thief crops.</param>
        /// <param name="placedItemTileKey">The tile key of the crop to thief.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ThiefCropAsync(string userId, string placedItemTileKey)
        {
            var paramsObj = new NakamaRpcService.ThiefCropRpcAsyncParams
            {
                userId            = userId,
                placedItemTileKey = placedItemTileKey
            };

            var response = await NakamaRpcService.Instance.ThiefCropRpcAsync(paramsObj);
            NakamaUserService.Instance.LoadInventoriesAsync();
            await NakamaSocketService.Instance.ForceCentralBroadcastInstantlyRpcAsync();
            // Handle response if necessary
        }

        #endregion

        #region Helping Others

        /// <summary>
        /// Helps another user by watering their crops.
        /// </summary>
        /// <param name="userId">The ID of the user to help.</param>
        /// <param name="placedItemTileKey">The tile key of the crop to water.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task HelpWaterAsync(string userId, string placedItemTileKey)
        {
            var paramsObj = new NakamaRpcService.HelpWaterRpcAsyncParams
            {
                userId            = userId,
                placedItemTileKey = placedItemTileKey
            };

            await NakamaRpcService.Instance.HelpWaterRpcAsync(paramsObj);
            await NakamaSocketService.Instance.ForceCentralBroadcastInstantlyRpcAsync();
            // Additional logic can be added here if needed
        }

        /// <summary>
        /// Helps another user by using pesticide on their crops.
        /// </summary>
        /// <param name="userId">The ID of the user to help.</param>
        /// <param name="placedItemTileKey">The tile key where pesticide is applied.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task HelpUsePesticideAsync(string userId, string placedItemTileKey)
        {
            var paramsObj = new NakamaRpcService.HelpUsePestisideRpcAsyncParams
            {
                userId            = userId,
                placedItemTileKey = placedItemTileKey
            };

            await NakamaRpcService.Instance.HelpUsePestisideRpcAsync(paramsObj);
            await NakamaSocketService.Instance.ForceCentralBroadcastInstantlyRpcAsync();

            // Additional logic can be added here if needed
        }

        /// <summary>
        /// Helps another user by using herbicide on their crops.
        /// </summary>
        /// <param name="userId">The ID of the user to help.</param>
        /// <param name="placedItemTileKey">The tile key where herbicide is applied.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task HelpUseHerbicideAsync(string userId, string placedItemTileKey)
        {
            var paramsObj = new NakamaRpcService.HelpUseHerbicideRpcAsyncParams
            {
                userId            = userId,
                placedItemTileKey = placedItemTileKey
            };

            await NakamaRpcService.Instance.HelpUseHerbicideRpcAsync(paramsObj);
            await NakamaSocketService.Instance.ForceCentralBroadcastInstantlyRpcAsync();

            // Additional logic can be added here if needed
        }

        #endregion

        public async Task BuyShopItem(string itemKey, int quantity =1 )
        {
            var resultData = await NakamaRpcService.Instance.BuySeedsRpcAsync(
                new NakamaRpcService.BuySeedsRpcAsyncParams
                {
                    key      = itemKey,
                    quantity = quantity
                });
            NakamaUserService.Instance.LoadWalletAsync();
            NakamaUserService.Instance.LoadInventoriesAsync();
        }
    }
}