using System;
using System.Collections.Generic;
using CiFarm.Scripts.Services;
using CiFarm.Scripts.Services.GameDatas;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.UI.Popups.Inventory;
using CiFarm.Scripts.UI.View;
using Imba.UI;
using SuperScrollView;
using UnityEngine;
using UnityEngine.Events;

namespace CiFarm.Scripts.UI.Popups
{
    public class InventoryPopup : UIPopup
    {
        [SerializeField] private List<InventoryTab> inventoryTabs;
        [SerializeField] private LoopGridView       loopGridView;

        public List<InvenItemData> inventoryItemsData;

        private UnityAction _onClose;

        protected override void OnInit()
        {
            base.OnInit();
            loopGridView.InitGridView(0, OnGetItemByIndex);
        }

        protected override void OnShowing()
        {
            base.OnShowing();
            if (Parameter != null)
            {
                var param = (GameViewParam)Parameter;
                _onClose = param.callBack;
            }

            OnClickInventoryTab(inventoryTabs[0]);
            LoadAllUserItem();
        }

        public void OnClickInventoryTab(InventoryTab tab)
        {
            foreach (var ivTab in inventoryTabs)
            {
                ivTab.SetSelect(ivTab == tab);
            }
        }

        private LoopGridViewItem OnGetItemByIndex(LoopGridView listView, int indexParam, int indexParam2,
            int indexParam3)
        {
            var index = indexParam;
            if (index < 0 || index >= inventoryItemsData.Count)
            {
                return null;
            }

            var itemData = inventoryItemsData[index];

            if (itemData == null)
            {
                return null;
            }

            var item       = listView.NewListViewItem("InventoryItem");
            var itemScript = item.GetComponent<InventoryItem>();

            itemScript.InitData(itemData, OnClickItem);
            return item;
        }

        private void ResetGridView()
        {
            loopGridView.RecycleAllItem();
            loopGridView.SetListItemCount(inventoryItemsData.Count);
            loopGridView.MovePanelToItemByIndex(0, 0);
        }

        public void OnClickItem(InvenItemData data)
        {
        }

        #region NAKAMA

        public void LoadAllUserItem()
        {
            inventoryItemsData.Clear();

            var rawData = NakamaAssetService.Instance.inventories;
            foreach (var data in rawData)
            {
                ModelConfigEntity gameConfig;
                switch (data.type)
                {
                    case InventoryType.Seed:
                        gameConfig = ResourceService.Instance.ModelGameObjectConfig.GetPlant(data.key);
                        break;
                    case InventoryType.Tile:
                        gameConfig = ResourceService.Instance.ModelGameObjectConfig.GetTile(data.key);
                        break;
                    case InventoryType.Animal:
                        gameConfig = ResourceService.Instance.ModelGameObjectConfig.GetPlant(data.key);
                        break;
                    default:
                        continue;
                }

                inventoryItemsData.Add(new InvenItemData
                {
                    itemKey  = data.key,
                    quantity = data.quantity,
                    iconItem = gameConfig.GameShopIcon
                });
            }

            ResetGridView();
        }

        public void LoadAllUserItemBySeed()
        {
            inventoryItemsData.Clear();
            inventoryItemsData.Clear();

            var rawData = NakamaAssetService.Instance.inventories;
            foreach (var data in rawData)
            {
                ModelConfigEntity gameConfig;
                switch (data.type)
                {
                    case InventoryType.Seed:
                        gameConfig = ResourceService.Instance.ModelGameObjectConfig.GetPlant(data.key);
                        break;
                    default:
                        continue;
                }

                inventoryItemsData.Add(new InvenItemData
                {
                    itemKey  = data.key,
                    quantity = data.quantity,
                    iconItem = gameConfig.GameShopIcon
                });
            }

            ResetGridView();
            ResetGridView();
        }

        public void LoadAllUserItemByAnimal()
        {
            inventoryItemsData.Clear();
            ResetGridView();
        }

        public void LoadAllUserItemByTile()
        {
            inventoryItemsData.Clear();
            ResetGridView();
        }

        public void LoadAllUserItemByProduct()
        {
            inventoryItemsData.Clear();
            ResetGridView();
        }

        public void LoadAllUserItemByTool()
        {
            inventoryItemsData.Clear();
            ResetGridView();
        }

        #endregion
    }

    [System.Serializable]
    public class InvenItemData
    {
        public string itemKey;
        public int    quantity;
        public Sprite iconItem;
    }
}