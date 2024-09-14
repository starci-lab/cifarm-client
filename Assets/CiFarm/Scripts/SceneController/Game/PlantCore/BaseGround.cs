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
        [SerializeField] private BasePlant plant;

        private bool       isPlanted  = false;
        private PlacedItem _dirtData ;

        public void Init(PlacedItem placedItem)
        {
            _dirtData = placedItem;
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
            if (EventSystem.current.IsPointerOverGameObject())
            {
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
        }

        private async void OnConfirmSetPlant(InvenItemData plantData)
        {
            DLogger.Log("Planting Item: " + plantData.inventoryKey +" To: " +_dirtData.key, "SHOP");

            try
            {
                var resultData = await NakamaRpcService.Instance.PlantSeedRpcAsync(
                 new NakamaRpcService.PlantSeedRpcAsyncParams
                 {
                     inventorySeedKey  = plantData.inventoryKey,
                     placedItemTileKey = _dirtData.key
                 });

                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
            }
            catch (Exception e)
            {
                DLogger.LogError("Planting Item error: " + e.Message, "Gound");
            }
        }
    }
}