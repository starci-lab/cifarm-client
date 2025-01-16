using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CiFarm.Core.Databases;
using CiFarm.Utils;
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
    }
}
