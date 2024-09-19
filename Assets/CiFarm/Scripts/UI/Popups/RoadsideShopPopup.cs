using System;
using System.Collections.Generic;
using System.Linq;
using CiFarm.Scripts.Services;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.UI.Popups.Roadside;
using CiFarm.Scripts.UI.View;
using CiFarm.Scripts.Utilities;
using Imba.Audio;
using Imba.UI;
using UnityEngine;
using UnityEngine.Events;

namespace CiFarm.Scripts.UI.Popups
{
    public class RoadsideShopPopup : UIPopup
    {
        [SerializeField] private List<RoadsideItem> roadsideItems;

        [Unity.Collections.ReadOnly] public List<RoadSideItemData> roadsideData;

        private UnityAction _onClose;

        private DeliveringProduct rawDelivering;

        protected override void OnInit()
        {
            base.OnInit();
            for (int i = 0; i < roadsideItems.Count; i++)
            {
                var index = i;
                roadsideItems[i].InitCallback(() => { OnPutNewItemOnSale(index); },
                    () => { OnClickToRemoveItemOnSale(index); });
            }
        }

        protected override void OnShowing()
        {
            base.OnShowing();
            if (Parameter != null)
            {
                var param = (GameViewParam)Parameter;
                _onClose = param.callBack;
            }

            LoadItemsOnSale();
        }

        private void LoadItemsOnSale()
        {
            roadsideData = new List<RoadSideItemData>();
            var rawData = NakamaRoadsideShopService.Instance.deliveringProducts;

            foreach (var delivering in rawData)
            {
                var spriteRender = ResourceService.Instance.ModelGameObjectConfig.GetPlant(delivering.referenceKey)
                    .GameHarvestIcon;

                roadsideData.Add(new RoadSideItemData
                {
                    Index             = delivering.index,
                    Key               = delivering.key,
                    ReferenceKey      = delivering.referenceKey,
                    SpriteItemProduct = spriteRender,
                    Quantity          = delivering.quantity
                });
            }

            // Load to display 
            for (int i = 0; i < roadsideItems.Count; i++)
            {
                var item = roadsideData.FirstOrDefault(o => o.Index == i);
                if (item == null)
                {
                    roadsideItems[i].SetProductOnSale();
                }
                else
                {
                    roadsideItems[i].SetProductOnSale(item.SpriteItemProduct, item.Quantity);
                }
            }
        }

        /// <summary>
        /// Button method
        /// </summary>
        /// <param name="index"></param>
        private void OnPutNewItemOnSale(int index)
        {
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PlantingPopup, new CustomInventoryPopupParam
            {
                PlantingPopupType = PlantingPopupType.Selling,
                PlantAction       = (data) => { OnConfirmSetSell(data, index); }
            });
        }

        /// <summary>
        /// Button method
        /// </summary>
        /// <param name="index"></param>
        private void OnClickToRemoveItemOnSale(int index)
        {
            var itemToRemove = roadsideData.FirstOrDefault(o => o.Index == index);

            UIManager.Instance.PopupManager.ShowMessageDialog("Confirm",
                "Are you sure want to remove this item from stock?", UIMessageBox.MessageBoxType.Yes_No,
                (result) =>
                {
                    if (result == UIMessageBox.MessageBoxAction.Accept)
                    {
                        OnConfirmRemoveItemSell(itemToRemove, index);
                    }

                    return true;
                });
        }

        protected override void OnHiding()
        {
            base.OnHiding();
            _onClose?.Invoke();
        }

        #region NAKAMA

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="plantData"></param>
        /// <param name="index"></param>
        private void OnConfirmSetSell(InvenItemData plantData, int index)
        {
            try
            {
                // call NAKAMA
                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
                var currentDelivering = new List<Services.NakamaServices.Inventory>();
                currentDelivering.Add(new Services.NakamaServices.Inventory
                {
                    key          = plantData.inventoryKey,
                    referenceKey = plantData.itemKey,
                    type         = plantData.type,
                    quantity     = plantData.quantity
                });

                NakamaRoadsideShopService.Instance.DeliverProductsAsync(currentDelivering);
            }
            catch (Exception e)
            {
                DLogger.LogError("OnConfirmSetSell Item error: " + e.Message, "Roadside");
            }
        }

        /// <summary>
        /// todo
        /// </summary>
        /// <param name="removeItem"></param>
        /// <param name="index"></param>
        private void OnConfirmRemoveItemSell(RoadSideItemData removeItem, int index)
        {
            try
            {
                var removeData =
                    NakamaRoadsideShopService.Instance.deliveringProducts.FirstOrDefault(o => o.key == removeItem.Key);
                NakamaRoadsideShopService.Instance.RetainProductsAsync(new List<DeliveringProduct>()
                {
                    removeData
                });
                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
            }
            catch (Exception e)
            {
                DLogger.LogError("OnConfirmSetSell Item error: " + e.Message, "Roadside");
            }
        }

        #endregion
    }

    [Serializable]
    public class RoadSideItemData
    {
        public int    Index;
        public string Key;
        public string ReferenceKey;
        public Sprite SpriteItemProduct;
        public int    Quantity;
    }
}