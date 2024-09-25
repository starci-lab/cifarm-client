using System;
using System.Collections.Generic;
using System.Linq;
using CiFarm.Scripts.Utilities;
using UnityEngine;

namespace CiFarm.Scripts.Services.GameDatas
{
    [CreateAssetMenu(fileName = "ModelGameConfig", menuName = "GameDatas/ModelConfig", order = 1)]
    public class ModelGameObjectConfig : ScriptableObject
    {
        [SerializeField] private List<ModelConfigEntity> tileMapper;
        [SerializeField] private List<ModelConfigEntity> plantMapper;

        public List<ModelConfigEntity> TileMapper    => tileMapper;
        public List<ModelConfigEntity> PlantMapper   => plantMapper;

        public GameObject GetTileObjectModel(string keyToFind)
        {
            var result = TileMapper.FirstOrDefault(o => o.Key == keyToFind);
            if (result == null)
            {
                DLogger.LogError("GetTile not found for: " + keyToFind, "ModelGameObjectConfig");
                return TileMapper[0].PrefabModel;
            }

            return result.PrefabModel;
        }

        public GameObject GetPlantObjectModel(string keyToFind)
        {
            var result = PlantMapper.FirstOrDefault(o => o.Key == keyToFind);
            if (result == null)
            {
                DLogger.LogError("GetPlant not found for: " + keyToFind, "ModelGameObjectConfig");
                return PlantMapper[0].PrefabModel;
            }

            return result.PrefabModel;
        }

        public ModelConfigEntity GetPlant(string keyToFind)
        {
            var result = PlantMapper.FirstOrDefault(o => o.Key == keyToFind);
            if (result == null)
            {
                DLogger.LogError("GetPlant not found for: " + keyToFind, "ModelGameObjectConfig");
                return PlantMapper[0];
            }

            return result;
        }

        public ModelConfigEntity GetTile(string keyToFind)
        {
            var result = tileMapper.FirstOrDefault(o => o.Key == keyToFind);
            if (result == null)
            {
                DLogger.LogError("GetTile not found for: " + keyToFind, "ModelGameObjectConfig");
                return PlantMapper[0];
            }

            return result;
        }

    }

    [Serializable]
    public class ModelConfigEntity
    {
        [SerializeField] private string     key;
        [SerializeField] private string     itemName;
        [SerializeField] private Vector2Int size = Vector2Int.one;
        [SerializeField] private GameObject prefabModel;
        [SerializeField] private Sprite     gameShopIcon;
        [SerializeField] private Sprite     gameHarvestIcon;

        public string Key      => key;
        public string ItemName => itemName;
        public Vector2Int Size => size;

        public GameObject PrefabModel => prefabModel;

        public Sprite GameShopIcon => gameShopIcon;

        public Sprite GameHarvestIcon => gameHarvestIcon;
    }
}