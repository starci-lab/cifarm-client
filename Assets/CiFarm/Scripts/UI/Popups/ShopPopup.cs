using System.Collections.Generic;
using CiFarm.Scripts.UI.Popups.Shop;
using CiFarm.Scripts.UI.View;
using Imba.UI;
using SuperScrollView;
using UnityEngine;
using UnityEngine.Events;

namespace CiFarm.Scripts.UI.Popups
{
    public class ShopPopup : UIPopup
    {
        [SerializeField] private LoopListView2 shopItemLoopListView;

        private UnityAction _onClose;

        [Header("TEST")]
        public List<ShopItemData> shopItemsData;

        protected override void OnInit()
        {
            base.OnInit();
            shopItemLoopListView.InitListView(0, OnGetItemByIndex);
            if (Parameter != null)
            {
                var param = (GameViewParam)Parameter;
                _onClose = param.callBack;
            }
        }

        protected override void OnShowing()
        {
            base.OnShowing();
            LoadAllItemShop();
        }

        public void OnClickBuyItem(ShopItemData item)
        {
        }

        LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
        {
            if (index < 0 || index >= shopItemsData.Count)
            {
                return null;
            }

            ShopItemData itemData = shopItemsData[index];

            if (itemData == null)
            {
                return null;
            }

            LoopListViewItem2 item;
            item = listView.NewListViewItem("ShopItem");

            var itemScript = item.GetComponent<ShopItem>();


            itemScript.InitData(itemData, OnClickBuyItem);
            return item;
        }

        protected override void OnHidden()
        {
            base.OnHidden();
            _onClose?.Invoke();
        }

        #region NAKAMA COMMUNICATE

        public void LoadAllItemShop()
        {
        }

        public void LoadItemShopByType()
        {
        }

        #endregion
    }

    [System.Serializable]
    public class ShopItemData
    {
        public string textItemName;
        public string textItemTimeDetail;
        public string textItemProfitDetail;
        public string textItemPrice;
        public Sprite iconItem;
    }
}