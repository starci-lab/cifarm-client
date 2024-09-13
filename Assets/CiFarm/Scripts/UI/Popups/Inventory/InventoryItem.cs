using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CiFarm.Scripts.UI.Popups.Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image           itemIcon;
        [SerializeField] private TextMeshProUGUI quantity;

        private InvenItemData              invenItemData;
        private UnityAction<InvenItemData> onClick;

        public void InitData(InvenItemData data, UnityAction<InvenItemData> callBack)
        {
            invenItemData   = data;
            onClick         = callBack;
            itemIcon.sprite = data.iconItem;
            quantity.text   = data.quantity.ToString();
        }

        public void OnClick()
        {
            onClick?.Invoke(invenItemData);
        }
    }
}