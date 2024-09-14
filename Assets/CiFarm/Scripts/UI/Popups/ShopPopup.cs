using System;
using System.Collections.Generic;
using CiFarm.Scripts.Services;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.UI.Popups.Shop;
using CiFarm.Scripts.UI.View;
using CiFarm.Scripts.Utilities;
using DG.Tweening;
using Imba.Audio;
using Imba.UI;
using SuperScrollView;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CiFarm.Scripts.UI.Popups
{
    public class ShopPopup : UIPopup
    {
        [SerializeField] private TextMeshProUGUI userWallet;
        [SerializeField] private LoopListView2 shopItemLoopListView;
        [SerializeField] private ShopTab       seedTab;
        [SerializeField] private ShopTab       animaTab;
        [SerializeField] private ShopTab       treeTab;

        private UnityAction        _onClose;
        public  List<ShopItemData> shopItemsData;

        public int _currentCoin = 0;
        protected override void OnInit()
        {
            base.OnInit();
            shopItemLoopListView.InitListView(0, OnGetItemByIndex);
            NakamaAssetService.Instance.onGoldChange = (FetchUserCoin);
        }

        protected override void OnShowing()
        {
            base.OnShowing();
            if (Parameter != null)
            {
                var param = (GameViewParam)Parameter;
                _onClose = param.callBack;
            }

            ClearSelectedShop();
            seedTab.SetSelect(true);
            LoadItemShopSeed();
            FetchUserCoin();
        }

        private void ResetListView()
        {
            shopItemLoopListView.RecycleAllItem();
            shopItemLoopListView.SetListItemCount(shopItemsData.Count);
            shopItemLoopListView.MovePanelToItemIndex(0, 0);
        }

        public void ClearSelectedShop()
        {
            seedTab.SetSelect();
            animaTab.SetSelect();
            treeTab.SetSelect();
        }

        private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
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

        protected override void OnHiding()
        {
            base.OnHiding();
            AudioManager.Instance.PlaySFX(AudioName.Close1);
            _onClose?.Invoke();
        }

        protected override void OnHidden()
        {
            base.OnHidden();
        }
        public void FetchUserCoin()
        {
            var targetCoin = NakamaAssetService.Instance.golds;
            DOTween.To(() => _currentCoin, x => _currentCoin = x, targetCoin, 0.3f)
                .OnUpdate(() => { userWallet.text = _currentCoin.ToString(); });
        }
        #region NAKAMA COMMUNICATE

        public void LoadItemShopSeed()
        {
            ClearSelectedShop();
            seedTab.SetSelect(true);
            var rawData = NakamaLoaderService.Instance.seeds;
            shopItemsData.Clear();
            foreach (var data in rawData)
            {
                var gameConfig = ResourceService.Instance.ModelGameObjectConfig.GetPlant(data.key);
                shopItemsData.Add(new ShopItemData
                {
                    itemKey              = data.key,
                    textItemName         = gameConfig.ItemName,
                    textItemTimeDetail   = (data.growthStageDuration).ToString(),
                    textItemProfitDetail = data.maxHarvestQuantity.ToString(),
                    textItemPrice        = data.price.ToString(),
                    iconItem             = gameConfig.GameShopIcon
                });
            }

            ResetListView();
        }

        public void LoadItemShopByAnimal()
        {
            ClearSelectedShop();
            animaTab.SetSelect(true);
            
            shopItemsData.Clear();
            ResetListView();
        }

        public void LoadItemShopByTree()
        {
            ClearSelectedShop();
            treeTab.SetSelect(true);
            
            shopItemsData.Clear();
            ResetListView();
        }

        public async void OnClickBuyItem(ShopItemData item)
        {
            DLogger.Log("Buy Item: " + item.textItemName, "SHOP");

            try
            {
                var resultData = await NakamaRpcService.Instance.BuySeedRpcAsync(
                    new NakamaRpcService.BuySeedRpcAsyncParams
                    {
                        key      = item.itemKey,
                        quantity = 1
                    });

                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
            }
            catch (Exception e)
            {
                DLogger.LogError("Buy Item error: " + e.Message, "SHOP");
            }
        }

        #endregion
    }

    [System.Serializable]
    public class ShopItemData
    {
        public string itemKey;
        public string textItemName;
        public string textItemTimeDetail;
        public string textItemProfitDetail;
        public string textItemPrice;
        public Sprite iconItem;
    }
}