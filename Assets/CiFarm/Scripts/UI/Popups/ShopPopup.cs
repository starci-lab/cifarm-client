using System.Collections.Generic;
using CiFarm.Scripts.UI.Popups.Shop;
using Imba.UI;
using SuperScrollView;
using UnityEngine;

namespace CiFarm.Scripts.UI.Popups
{
    public class ShopPopup : UIPopup
    {
        [SerializeField] private LoopListView2 shopItemLoopListView;

        [Header("TEST")]
        public List<ShopItemData> shopItemsData;

        protected override void OnInit()
        {
            base.OnInit();

            shopItemLoopListView.InitListView(0, OnGetItemByIndex);
            shopItemLoopListView.SetListItemCount(shopItemsData.Count);
            shopItemLoopListView.MovePanelToItemIndex(0, 0);
        }

        protected override void OnShowing()
        {
            base.OnShowing();
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