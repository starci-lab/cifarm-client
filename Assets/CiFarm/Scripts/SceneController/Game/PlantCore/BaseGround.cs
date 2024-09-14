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
        public                   BasePlant plant;

        private bool       isPlanted = false;
        public  PlacedItem dirtData;

        public void Init(PlacedItem placedItem)
        {
            dirtData  = placedItem;
            isPlanted = false;
        }

        public void SetPlant(BasePlant plantToSet)
        {
            plant = plantToSet;
            plant.transform.SetParent(transform);
            plant.transform.position = positionPlant.position;
            isPlanted                = true;
        }

        private void OnMouseDown()
        {
            DLogger.Log("Ground Clicked");
            if (EventSystem.current.IsPointerOverGameObject())
            {
                DLogger.Log("Ground Clicked on ui");

                return;
            }

            if (!isPlanted)
            {
                UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PlantingPopup, new PlantingPopupParam
                {
                    CloseAction = null,
                    PlantAction = OnConfirmSetPlant
                });
            }

            DLogger.Log("Ground Clicked is planted");
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
                DLogger.LogError("Planting Item error: " + e.Message, "Gound");
            }
        }
    }
}