using System.Collections.Generic;
using CiFarm.Scripts.Services;
using CiFarm.Scripts.Services.GameDatas;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.UI.Popups.Inventory;
using CiFarm.Scripts.UI.View;
using CiFarm.Scripts.Utilities;
using Imba.UI;
using SuperScrollView;
using UnityEngine;
using UnityEngine.Events;

namespace CiFarm.Scripts.UI.Popups
{
    public class PlantingPopupParam
    {
        public UnityAction                CloseAction;
        public UnityAction<InvenItemData> PlantAction;
    }

    public class PlantingPopup : UIPopup
    {
        [SerializeField] private LoopGridView loopGridView;

        public List<InvenItemData> inventoryItemsData;

        private UnityAction                _onClose;
        private UnityAction<InvenItemData> _plantAction;

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
                var param = (PlantingPopupParam)Parameter;
                _onClose     = param.CloseAction;
                _plantAction = param.PlantAction;
            }

            LoadAllUserItem();
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
            DLogger.Log("Clicked Game item: " + data.itemKey);
            _plantAction?.Invoke(data);
            Hide();
        }

        #region NAKAMA

        public void LoadAllUserItem()
        {
            inventoryItemsData.Clear();

            var rawData = NakamaAssetService.Instance.inventories;
            if (rawData == null || rawData.Count == 0)
            {
                rawData = new ();
            }
            foreach (var data in rawData)
            {
                ModelConfigEntity gameConfig;
                switch (data.type)
                {
                    case InventoryType.Seed:
                        gameConfig = ResourceService.Instance.ModelGameObjectConfig.GetPlant(data.referenceKey);
                        break;
                    default:
                        continue;
                }

                inventoryItemsData.Add(new InvenItemData
                {
                    inventoryKey = data.key,
                    itemKey      = data.referenceKey,
                    quantity     = data.quantity,
                    iconItem     = gameConfig.GameShopIcon
                });
            }

            ResetGridView();
        }

        #endregion
    }
}