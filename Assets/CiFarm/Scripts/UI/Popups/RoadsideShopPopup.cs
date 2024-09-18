using System.Collections.Generic;
using CiFarm.Scripts.UI.Popups.Roadside;
using Imba.UI;
using UnityEngine;

namespace CiFarm.Scripts.UI.Popups
{
    public class RoadsideShopPopup : UIPopup
    {
        protected override void OnInit()
        {
            base.OnInit();
        }

        protected override void OnShowing()
        {
            base.OnShowing();
        }

        [SerializeField] private List<RoadsideItem> roadsideItems;

        public void LoadItemsOnSale()
        {
            
        }
        public void OnPutNewItemOnSale(int index)
        {
        }

        public void OnClickToRemoveItemOnSale(int index)
        {
        }
        
        
    }
}