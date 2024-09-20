using System;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.UI.Popups;
using CiFarm.Scripts.Utilities;
using Imba.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CiFarm.Scripts.SceneController.Game.PlantCore
{
    public class BaseGround : MonoBehaviour
    {
        [SerializeField] private Transform  positionPlant;
        [SerializeField] private GameObject dirtBubbleModel;

        [ReadOnly]
        public BasePlant plant;

        [ReadOnly]
        public PlacedItem dirtData;

        public void Init(PlacedItem placedItem)
        {
            dirtData = placedItem;
            DirtBubble bubble;
            if (dirtData.fullyMatured)
            {
                bubble = SpawnBubble();
                bubble.SetBubble(dirtData.key, InjectionType.TextQuantity,
                    currentQuantity: dirtData.seedGrowthInfo.harvestQuantityRemaining,
                    maxQuantity: dirtData.seedGrowthInfo.crop.maxHarvestQuantity
                );
            }
            else
            {
                switch (placedItem.seedGrowthInfo.plantCurrentState)
                {
                    case PlantCurrentState.NeedWater:
                        bubble = SpawnBubble();
                        bubble.SetBubble(dirtData.key, InjectionType.Water);
                        break;
                    case PlantCurrentState.IsWeedy:
                        bubble = SpawnBubble();
                        bubble.SetBubble(dirtData.key, InjectionType.Grass);
                        break;
                    case PlantCurrentState.IsInfested:
                        bubble = SpawnBubble();
                        bubble.SetBubble(dirtData.key, InjectionType.Worm);
                        break;
                }
            }
        }

        public void SetPlant(BasePlant plantToSet)
        {
            plant = plantToSet;
            plant.transform.SetParent(transform);
            plant.transform.position = positionPlant.position;
        }

        public void RemovePlant()
        {
            SimplePool.Despawn(plant.gameObject);
            dirtData.isPlanted = false;
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            GameController.Instance.OnClickGround(this);
        }

        public DirtBubble SpawnBubble()
        {
            var dirtBubbleObj = SimplePool.Spawn(dirtBubbleModel, transform.position, Quaternion.identity);
            return dirtBubbleObj.GetComponent<DirtBubble>();
        }

        public async void OnConfirmSetPlant(InvenItemData plantData)
        {
            DLogger.Log("Planting Item: " + plantData.inventoryKey + " To: " + dirtData.key, "SHOP");

            try
            {
                var resultData = await NakamaRpcService.Instance.PlantSeedRpcAsync(
                    new NakamaRpcService.PlantSeedRpcAsyncParams
                    {
                        inventorySeedKey  = plantData.inventoryKey,
                        placedItemTileKey = dirtData.key
                    });
                await NakamaRpcService.Instance.ForceCentralBroadcastInstantlyRpcAsync();
                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
            }
            catch (Exception e)
            {
                DLogger.LogError("Planting Item error: " + e.Message, "Ground");
            }
        }
    }
}