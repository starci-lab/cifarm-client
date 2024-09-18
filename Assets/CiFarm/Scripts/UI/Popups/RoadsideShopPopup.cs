using System.Collections.Generic;
using System.Linq;
using CiFarm.Scripts.UI.Popups.Roadside;
using CiFarm.Scripts.UI.View;
using Imba.UI;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CiFarm.Scripts.UI.Popups
{
    public class RoadsideShopPopup : UIPopup
    {
        [SerializeField] private List<RoadsideItem> roadsideItems;

        [ReadOnly] public List<RoadSideItemData> RoadsideData;

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
            RoadsideData = new List<RoadSideItemData>();

            // Load to display 
            for (int i = 0; i < roadsideItems.Count; i++)
            {
                var item = RoadsideData.FirstOrDefault(o => o.Index == i);
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
        }

        public void OnClickToRemoveItemOnSale(int index)
        {
        }

        protected override void OnHiding()
        {
            base.OnHiding();
            _onClose?.Invoke();
        }
    }

    public class RoadSideItemData
    {
        public int    Index;
        public string Key;
        public string ReferenceKey;
        public Sprite SpriteItemProduct;
        public int    Quantity;
    }
}