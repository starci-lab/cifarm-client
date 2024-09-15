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
        [SerializeField] private Transform positionPlant;

        public BasePlant  plant;
        public PlacedItem dirtData;

        public void Init(PlacedItem placedItem)
        {
            dirtData = placedItem;
        }

        public void SetPlant(BasePlant plantToSet)
        {
            plant = plantToSet;
            plant.transform.SetParent(transform);
            plant.transform.position = positionPlant.position;
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            // Trồng cây
            if (!dirtData.isPlanted)
            {
                UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PlantingPopup, new PlantingPopupParam
                {
                    CloseAction = null,
                    PlantAction = OnConfirmSetPlant
                });
                return;
            }

            // Thu hoạch
            if (dirtData.isPlanted && dirtData.fullyMatured)
            {
                DLogger.Log("Try Harvesting...");
                OnHarvestPlant();
            }
        }

        private async void OnConfirmSetPlant(InvenItemData plantData)
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

        private async void OnHarvestPlant()
        {
            try
            {
                //GameController.Instance.OnFetchPlacedDataFromServer();
                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
            }
            catch (Exception e)
            {
                DLogger.LogError("Harvest Item error: " + e.Message, "Ground");
            }
        }
    }
}