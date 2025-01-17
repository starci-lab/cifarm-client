using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CiFarm.Core.Databases;
using CiFarm.Utils;
using Cysharp.Threading.Tasks;
using Imba.Utils;
using UnityEngine;

namespace CiFarm.DataManager
{
    public class StaticDataManager : ManualSingletonMono<StaticDataManager>
    {
        /// <summary>
        /// List of animals
        /// </summary>
        [SerializeField]
        private List<AnimalEntity> _animals;

        public List<AnimalEntity> Animals
        {
            get => _animals;
            set => _animals = value;
        }

        /// <summary>
        /// List of buildings
        /// </summary>
        [SerializeField]
        private List<BuildingEntity> _buildings;

        /// <summary>
        /// List of crops
        /// </summary>
        [SerializeField]
        private List<CropEntity> _crops;

        public List<CropEntity> Crops
        {
            get => _crops;
            set => _crops = value;
        }

        public List<BuildingEntity> Buildings
        {
            get => _buildings;
            set => _buildings = value;
        }

        /// <summary>
        /// List of placed item types
        ///  </summary>
        [SerializeField]
        private List<PlacedItemTypeEntity> _placedItemTypes;

        public List<PlacedItemTypeEntity> PlacedItemTypes
        {
            get => _placedItemTypes;
            set => _placedItemTypes = value;
        }

        /// <summary>
        /// List of spin prizes
        /// </summary>
        [SerializeField]
        private List<SpinPrizeEntity> _spinPrizes;

        public List<SpinPrizeEntity> SpinPrizes
        {
            get => _spinPrizes;
            set => _spinPrizes = value;
        }

        /// <summary>
        /// List of spin slots
        /// </summary>
        [SerializeField]
        private List<SpinSlotEntity> _spinSlots;

        public List<SpinSlotEntity> SpinSlots
        {
            get => _spinSlots;
            set => _spinSlots = value;
        }

        /// <summary>
        /// List of supplies
        /// </summary>
        [SerializeField]
        private List<SupplyEntity> _supplies;

        public List<SupplyEntity> Supplies
        {
            get => _supplies;
            set => _supplies = value;
        }

        /// <summary>
        /// List of tiles
        /// </summary>
        [SerializeField]
        private List<TileEntity> _tiles;

        public List<TileEntity> Tiles
        {
            get => _tiles;
            set => _tiles = value;
        }

        /// <summary>
        /// List of tools
        /// </summary>
        [SerializeField]
        private List<ToolEntity> _tools;

        public List<ToolEntity> Tools
        {
            get => _tools;
            set => _tools = value;
        }

        /// <summary>
        /// List of inventory types
        /// </summary>
        [SerializeField]
        private List<InventoryTypeEntity> _inventoryTypes;

        public List<InventoryTypeEntity> InventoryTypes
        {
            get => _inventoryTypes;
            set => _inventoryTypes = value;
        }

        /// <summary>
        /// List of products
        /// </summary>
        [SerializeField]
        private List<ProductEntity> _products;

        public List<ProductEntity> Products
        {
            get => _products;
            set => _products = value;
        }

        /// <summary>
        /// List of daily rewards
        /// </summary>
        [SerializeField]
        private List<DailyRewardEntity> _dailyRewards;

        public List<DailyRewardEntity> DailyRewards
        {
            get => _dailyRewards;
            set => _dailyRewards = value;
        }

        /// <summary>
        /// List of upgrades
        /// </summary>
        [SerializeField]
        private List<UpgradeEntity> _upgrades;

        public List<UpgradeEntity> Upgrades
        {
            get => _upgrades;
            set => _upgrades = value;
        }

        /// <summary>
        /// Activities
        /// </summary>
        [SerializeField]
        private Activities _activities;

        public Activities Activities
        {
            get => _activities;
            set => _activities = value;
        }

        /// <summary>
        /// Animal Randomness
        /// </summary>
        [SerializeField]
        private AnimalRandomness _animalRandomness;

        public AnimalRandomness AnimalRandomness
        {
            get => _animalRandomness;
            set => _animalRandomness = value;
        }

        /// <summary>
        /// Crop Randomness
        /// </summary>
        [SerializeField]
        private CropRandomness _cropRandomness;

        public CropRandomness CropRandomness
        {
            get => _cropRandomness;
            set => _cropRandomness = value;
        }

        /// <summary>
        /// Starter
        /// </summary>
        [SerializeField]
        private Starter _starter;

        public Starter Starter
        {
            get => _starter;
            set => _starter = value;
        }

        /// <summary>
        /// Spin Info
        /// </summary>
        [SerializeField]
        private SpinInfo _spinInfo;

        public SpinInfo SpinInfo
        {
            get => _spinInfo;
            set => _spinInfo = value;
        }

        /// <summary>
        /// Energy Regen
        /// </summary>
        [SerializeField]
        private EnergyRegen _energyRegen;

        public EnergyRegen EnergyRegen
        {
            get => _energyRegen;
            set => _energyRegen = value;
        }

        public IEnumerator Start()
        {
            yield return new WaitUntil(
                () => CiFarmSDK.Instance != null && CiFarmSDK.Instance.Authenticated
            );

            FetchAnimalsAsync();
            FetchBuildingsAsync();
            FetchCropsAsync();
            FetchPlacedItemTypesAsync();
            FetchSpinPrizesAsync();
            FetchSpinSlotsAsync();
            FetchSuppliesAsync();
            FetchTilesAsync();
            FetchToolsAsync();
            FetchInventoryTypesAsync();
            FetchProductsAsync();
            FetchDailyRewardsAsync();
            FetchUpgradesAsync();
            FetchActivitiesAsync();
            FetchAnimalRandomnessAsync();
            FetchCropRandomnessAsync();
            FetchStarterAsync();
            FetchSpinInfoAsync();
            FetchEnergyRegenAsync();
        }

        public async void FetchAnimalsAsync()
        {
            Animals = await CiFarmSDK.Instance.GraphQLClient.QueryAnimalsAsync();
            ConsoleLogger.LogVerbose($"Fetched {Animals.Count} animals");
        }

        public async void FetchBuildingsAsync()
        {
            Buildings = await CiFarmSDK.Instance.GraphQLClient.QueryBuildingsAsync();
            ConsoleLogger.LogVerbose($"Fetched {Buildings.Count} buildings");
        }

        public async void FetchCropsAsync()
        {
            Crops = await CiFarmSDK.Instance.GraphQLClient.QueryCropsAsync();
            ConsoleLogger.LogVerbose($"Fetched {Crops.Count} crops");
        }

        public async void FetchPlacedItemTypesAsync()
        {
            PlacedItemTypes = await CiFarmSDK.Instance.GraphQLClient.QueryPlacedItemTypesAsync();
            ConsoleLogger.LogVerbose($"Fetched {PlacedItemTypes.Count} placed item types");
        }

        public async void FetchSpinPrizesAsync()
        {
            SpinPrizes = await CiFarmSDK.Instance.GraphQLClient.QuerySpinPrizesAsync();
            ConsoleLogger.LogVerbose($"Fetched {SpinPrizes.Count} spin prizes");
        }

        public async void FetchSpinSlotsAsync()
        {
            SpinSlots = await CiFarmSDK.Instance.GraphQLClient.QuerySpinSlotsAsync();
            ConsoleLogger.LogVerbose($"Fetched {SpinSlots.Count} spin slots");
        }

        public async void FetchSuppliesAsync()
        {
            Supplies = await CiFarmSDK.Instance.GraphQLClient.QuerySuppliesAsync();
            ConsoleLogger.LogVerbose($"Fetched {Supplies.Count} supplies");
        }

        public async void FetchTilesAsync()
        {
            Tiles = await CiFarmSDK.Instance.GraphQLClient.QueryTilesAsync();
            ConsoleLogger.LogVerbose($"Fetched {Tiles.Count} tiles");
        }

        public async void FetchToolsAsync()
        {
            Tools = await CiFarmSDK.Instance.GraphQLClient.QueryToolsAsync();
            ConsoleLogger.LogVerbose($"Fetched {Tools.Count} tools");
        }

        public async void FetchInventoryTypesAsync()
        {
            InventoryTypes = await CiFarmSDK.Instance.GraphQLClient.QueryInventoryTypesAsync();
            ConsoleLogger.LogVerbose($"Fetched {InventoryTypes.Count} inventory types");
        }

        public async void FetchProductsAsync()
        {
            Products = await CiFarmSDK.Instance.GraphQLClient.QueryProductsAsync();
            ConsoleLogger.LogVerbose($"Fetched {Products.Count} products");
        }

        public async void FetchDailyRewardsAsync()
        {
            DailyRewards = await CiFarmSDK.Instance.GraphQLClient.QueryDailyRewardsAsync();
            ConsoleLogger.LogVerbose($"Fetched {DailyRewards.Count} daily rewards");
        }

        public async void FetchUpgradesAsync()
        {
            Upgrades = await CiFarmSDK.Instance.GraphQLClient.QueryUpgradesAsync();
            ConsoleLogger.LogVerbose($"Fetched {Upgrades.Count} upgrades");
        }

        public async void FetchActivitiesAsync()
        {
            Activities = await CiFarmSDK.Instance.GraphQLClient.QueryActivitiesAsync();
            ConsoleLogger.LogVerbose("Fetched activities");
        }

        public async void FetchAnimalRandomnessAsync()
        {
            AnimalRandomness = await CiFarmSDK.Instance.GraphQLClient.QueryAnimalRandomnessAsync();
            ConsoleLogger.LogVerbose("Fetched animal randomness");
        }

        public async void FetchCropRandomnessAsync()
        {
            CropRandomness = await CiFarmSDK.Instance.GraphQLClient.QueryCropRandomnessAsync();
            ConsoleLogger.LogVerbose("Fetched crop randomness");
        }

        public async void FetchStarterAsync()
        {
            Starter = await CiFarmSDK.Instance.GraphQLClient.QueryStarterAsync();
            ConsoleLogger.LogVerbose("Fetched starter");
        }

        public async void FetchSpinInfoAsync()
        {
            SpinInfo = await CiFarmSDK.Instance.GraphQLClient.QuerySpinInfoAsync();
            ConsoleLogger.LogVerbose("Fetched spin info");
        }

        public async void FetchEnergyRegenAsync()
        {
            EnergyRegen = await CiFarmSDK.Instance.GraphQLClient.QueryEnergyRegenAsync();
            ConsoleLogger.LogVerbose("Fetched energy regen");
        }
    }
}
