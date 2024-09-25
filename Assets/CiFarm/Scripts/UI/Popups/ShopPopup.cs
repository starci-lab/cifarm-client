using System;
using System.Collections.Generic;
using CiFarm.Scripts.SceneController.Game;
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
        [SerializeField] private LoopListView2   shopItemLoopListView;
        [SerializeField] private ShopTab         seedTab;
        [SerializeField] private ShopTab         animaTab;
        [SerializeField] private ShopTab         buildingTab;
        [SerializeField] private ShopTab         treeTab;

        private UnityAction        _onClose;
        public  List<ShopItemData> shopItemsData;

        public int _currentCoin = 0;

        protected override void OnInit()
        {
            base.OnInit();
            shopItemLoopListView.InitListView(0, OnGetItemByIndex);
            NakamaUserService.Instance.onGoldChange = (FetchUserCoin);
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
            buildingTab.SetSelect();
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
            var targetCoin = NakamaUserService.Instance.golds;
            DOTween.To(() => _currentCoin, x => _currentCoin = x, targetCoin, 0.3f)
                .OnUpdate(() => { userWallet.text = _currentCoin.ToString(); });
        }

        #region NAKAMA COMMUNICATE

        public void LoadItemShopSeed()
        {
            ClearSelectedShop();
            seedTab.SetSelect(true);
            var rawData = NakamaLoaderService.Instance.crops;
            shopItemsData.Clear();
            foreach (var data in rawData)
            {
                var gameConfig = ResourceService.Instance.ModelGameObjectConfig.GetPlant(data.key);

                shopItemsData.Add(new ShopItemData
                {
                    itemKey      = data.key,
                    textItemName = gameConfig.ItemName,
                    shopType     = ShopType.Seed,
                    // textItemTimeDetail   = (data.growthStageDuration).ToString(),
                    // textItemProfitDetail = data.maxHarvestQuantity.ToString(),
                    textItemTimeDetail =
                        "Time: " + ((float)data.growthStageDuration / 60).ToString("F2") + " per stage",
                    textItemProfitDetail = "Products: " + data.maxHarvestQuantity,
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

        public void LoadItemShopByBuilding()
        {
            ClearSelectedShop();
            seedTab.SetSelect(true);
            var rawData = NakamaLoaderService.Instance.buildings;
            shopItemsData.Clear();
            foreach (var data in rawData)
            {
                if (!data.availableInShop)
                {
                    continue;
                }

                var gameConfig = ResourceService.Instance.ModelGameObjectConfig.GetConstruction(data.key);
                var detail     = ResourceService.Instance.ItemDetailConfig.GetItemDetail(data.key);
                shopItemsData.Add(new ShopItemData
                {
                    itemKey              = data.key,
                    shopType             = ShopType.Building,
                    textItemName         = gameConfig.ItemName,
                    textItemTimeDetail   = detail.ItemDescription,
                    textItemProfitDetail = "",
                    textItemPrice        = data.price.ToString(),
                    iconItem             = gameConfig.GameShopIcon
                });
            }

            ResetListView();
        }

        private void OnClickBuyItem(ShopItemData item)
        {
            //Validate
            if (_currentCoin < int.Parse(item.textItemPrice))
            {
                UIManager.Instance.PopupManager.ShowMessageDialog("Buy fail",
                    "You do not have enough gold to buy this item", UIMessageBox.MessageBoxType.OK);
                NakamaUserService.Instance.LoadWalletAsync();
                return;
            }

            DLogger.Log("Buy Item: " + item.textItemName, "SHOP");
            switch (item.shopType)
            {
                case ShopType.Seed:
                case ShopType.Animal:
                    BuyToInventory(item);
                    break;
                case ShopType.Building:
                    ConstructionBuilding(item);
                    break;
                case ShopType.Tree:
                    break;
            }
        }

        private async void BuyToInventory(ShopItemData item)
        {
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
                UIManager.Instance.PopupManager.ShowMessageDialog("Buy fail",
                    "You do not have enough gold to buy this item", UIMessageBox.MessageBoxType.OK);
                DLogger.LogWarning("Buy Item error: " + e.Message, "SHOP");
            }
        }

        private void ConstructionBuilding(ShopItemData item)
        {
            Hide(true);
            GameController.Instance.EnterEditMode(new InvenItemData
            {
                key          = item.itemKey,
                referenceKey = item.itemKey,
                quantity     = 1,
                isPremium    = false,
                isUnique     = false,
                type         = InventoryType.Building,
                iconItem     = item.iconItem
            });
        }

        #endregion
    }

    [System.Serializable]
    public class ShopItemData
    {
        public string   itemKey;
        public string   textItemName;
        public string   textItemTimeDetail;
        public string   textItemProfitDetail;
        public string   textItemPrice;
        public ShopType shopType;
        public Sprite   iconItem;
    }

    public enum ShopType
    {
        Seed,
        Animal,
        Building,
        Tree
    }
}