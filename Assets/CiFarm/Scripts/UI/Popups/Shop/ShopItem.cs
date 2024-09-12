using SuperScrollView;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CiFarm.Scripts.UI.Popups.Shop
{
    public class ShopItem : LoopListViewItem2
    {
        [SerializeField] private TextMeshProUGUI textItemName;
        [SerializeField] private TextMeshProUGUI textItemTimeDetail;
        [SerializeField] private TextMeshProUGUI textItemProfitDetail;
        [SerializeField] private TextMeshProUGUI textItemPrice;
        [SerializeField] private Image           iconItem;

        private ShopItemData              _shopItemData;
        private UnityAction<ShopItemData> _buyAction;

        public void InitData(ShopItemData data, UnityAction<ShopItemData> callBack)
        {
            _shopItemData             = data;
            _buyAction                = callBack;
            textItemName.text         = data.textItemName;
            textItemTimeDetail.text   = "Plant Time: " + data.textItemTimeDetail;
            textItemProfitDetail.text = "Profit: " + data.textItemProfitDetail;
            textItemPrice.text        = data.textItemPrice;
            iconItem.sprite           = data.iconItem;
        }
    
        public void OnBuy()
        {
            _buyAction?.Invoke(_shopItemData);
        }
    }
}