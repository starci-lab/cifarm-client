using System;
using System.Collections.Generic;
using System.Linq;
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

        public void LoadItemsOnSale()
        {
            // Load item from server
            roadsideData = new List<RoadSideItemData>();

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

        public void OnPutNewItemOnSale(int index)
        {
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PlantingPopup, new CustomInventoryPopupParam
            {
                PlantingPopupType = PlantingPopupType.Selling,
                PlantAction       = (data) => { OnConfirmSetSell(data, index); }
            });
        }

        public void OnClickToRemoveItemOnSale(int index)
        {
            var itemToRemove = roadsideData.FirstOrDefault(o => o.Index == index);
        }

        protected override void OnHiding()
        {
            base.OnHiding();
            _onClose?.Invoke();
        }

        #region NAKAMA
        public void OnConfirmSetSell(InvenItemData plantData, int index)
        {
            try
            {
                // call NAKAMA
                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
            }
            catch (Exception e)
            {
                DLogger.LogError("OnConfirmSetSell Item error: " + e.Message, "Roadside");
            }
        }

        public void OnConfirmRemoveItemSell(InvenItemData plantData, int index)
        {
            try
            {
                // call NAKAMA
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