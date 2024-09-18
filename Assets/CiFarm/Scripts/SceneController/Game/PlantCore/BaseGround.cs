using System;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.UI.Popups;
using CiFarm.Scripts.Utilities;
using Imba.Audio;
using Imba.UI;
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

            // Planting
            if (!dirtData.isPlanted)
            {
                UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PlantingPopup, new CustomInventoryPopupParam
                {
                    PlantingPopupType =  PlantingPopupType.Planting,
                    CloseAction = null,
                    PlantAction = OnConfirmSetPlant
                });
                return;
            }

            // Harvest
            if (dirtData.isPlanted && dirtData.fullyMatured)
            {
                GameController.Instance.OnHarvestPlant(this);
                return;
            }

            // Planted and everything normal
            if (dirtData.isPlanted && dirtData.seedGrowthInfo.plantCurrentState == PlantCurrentState.Normal)
            {
                var bubble = SpawnBubble();
                bubble.SetBubble(dirtData.key, InjectionType.Timer,
                    dirtData.seedGrowthInfo.seed.growthStageDuration -
                    (int)dirtData.seedGrowthInfo.currentStageTimeElapsed);
                return;
            }

            // REQUIRED SOME THING
            if (dirtData.isPlanted && dirtData.seedGrowthInfo.plantCurrentState != PlantCurrentState.Normal)
            {
                switch (dirtData.seedGrowthInfo.plantCurrentState)
                {
                    case PlantCurrentState.NeedWater:
                        GameController.Instance.OnWaterPlant(this);
                        break;
                    case PlantCurrentState.IsWeedy:
                        GameController.Instance.OnHerbicidePlant(this);
                        break;
                    case PlantCurrentState.IsInfested:
                        GameController.Instance.OnPesticidePlant(this);
                        break;
                }

                return;
            }
        }

        private DirtBubble SpawnBubble()
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
                //GameController.Instance.OnFetchPlacedDataFromServer();
                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
            }
            catch (Exception e)
            {
                DLogger.LogError("Planting Item error: " + e.Message, "Ground");
            }
        }
    }
}