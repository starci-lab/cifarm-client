using System;
using System.Collections.Generic;
using System.Linq;
using CiFarm.Scripts.Utilities;
using JetBrains.Annotations;
using UnityEngine;

namespace CiFarm.Scripts.Services.GameDatas
{
    [CreateAssetMenu(fileName = "ModelGameConfig", menuName = "GameDatas/ModelConfig", order = 1)]
    public class ModelGameObjectConfig : ScriptableObject
    {
        [SerializeField] private List<ModelConfigEntity> tileMapper;
        [SerializeField] private List<ModelConfigEntity> plantMapper;
        [SerializeField] private List<ModelConfigEntity> constructionMapper;

        public List<ModelConfigEntity> TileMapper         => tileMapper;
        public List<ModelConfigEntity> PlantMapper        => plantMapper;
        public List<ModelConfigEntity> ConstructionMapper => constructionMapper;

        public GameObject GetTile(string keyToFind)
        {
            var result = TileMapper.FirstOrDefault(o => o.Key == keyToFind);
            if (result == null)
            {
                DLogger.LogError("GetTile not found for: " + keyToFind, "ModelGameObjectConfig");
                return TileMapper[0].PrefabModel;
            }

            return result.PrefabModel;
        }

        public GameObject GetPlant(string keyToFind)
        {
            var result = PlantMapper.FirstOrDefault(o => o.Key == keyToFind);
            if (result == null)
            {
                DLogger.LogError("GetPlant not found for: " + keyToFind, "ModelGameObjectConfig");
                return PlantMapper[0].PrefabModel;
            }

            return result.PrefabModel;
        }

        public GameObject GetConstruction(string keyToFind)
        {
            var result = ConstructionMapper.FirstOrDefault(o => o.Key == keyToFind);
            if (result == null)
            {
                DLogger.LogError("GetConstruction not found for: " + keyToFind, "ModelGameObjectConfig");
                return ConstructionMapper[0].PrefabModel;
                ;
            }

            return result.PrefabModel;
        }
    }

    [Serializable]
    public class ModelConfigEntity
    {
        [SerializeField] private string     key;
        [SerializeField] private GameObject prefabModel;
        [SerializeField] private Sprite gameShopIcon;
        [SerializeField] private Sprite gameHarvestIcon;

        public string Key => key;

        public GameObject PrefabModel => prefabModel;

        public Sprite GameShopIcon => gameShopIcon;

        public Sprite GameHarvestIcon => gameHarvestIcon;
    }
}