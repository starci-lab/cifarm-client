using Imba.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CiFarm.Scripts.UI.Popups
{
    public class ItemDetailPopupParam
    {
        public string      ItemId;
        public int         Quantity;
        public Sprite      IconItem;
        public UnityAction OnSellItem;
    }

    public class ItemDetailPopup : UIPopup
    {
        [Header("Customize popup")]
        [SerializeField] private Image itemIcon;

        [SerializeField] private TextMeshProUGUI textItemName;
        [SerializeField] private TextMeshProUGUI textItemPrice;
        [SerializeField] private TMP_InputField  textItemCounter;
        [SerializeField] private TextMeshProUGUI textTotalValue;

        public int counter = 1;
        public int basePrice;

        private UnityAction _onSellItem;

        private int    _quantity;
        private string _itemId;

        protected override void OnShowing()
        {
            base.OnShowing();
            if (Parameter != null)
            {
                var param = (ItemDetailPopupParam)Parameter;
                itemIcon.sprite = param.IconItem;
                _itemId         = param.ItemId;
                _quantity       = param.Quantity;
                _onSellItem     = param.OnSellItem;
                basePrice       = 1;
            }

            textItemPrice.text = "Price: " + basePrice;
            UpdateCounter();
        }

        protected override void OnHiding()
        {
            base.OnHiding();
        }

        public void UpdateCounter()
        {
            textItemCounter.text = counter.ToString();
            textTotalValue.text  = "For: " + (basePrice * counter);
        }

        public void OnClickRight()
        {
            var temp = int.Parse(textItemCounter.text);
            if (temp != counter)
            {
                counter = temp;
            }

            counter++;
            if (counter > _quantity)
            {
                counter = _quantity;
            }

            UpdateCounter();
        }

        public void OnClickLeft()
        {
            var temp = int.Parse(textItemCounter.text);
            if (temp != counter)
            {
                counter = temp;
            }

            counter--;
            if (counter <= 0)
            {
                counter = 1;
            }

            UpdateCounter();
        }

        public void OnClickSell()
        {
            _onSellItem?.Invoke();
        }
    }
}