using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CiFarm.Scripts.UI.Popups.Roadside
{
    public class RoadsideItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textQuantity;
        [SerializeField] private GameObject      plusButton;
        [SerializeField] private GameObject      productGroup;
        [SerializeField] private Image           productRender;

        public void SetProductOnSale(Sprite sprite = null, int quantity = 0)
        {
            if (sprite != null && quantity != 0)
            {
                productGroup.SetActive(true);
                plusButton.SetActive(false);
                productRender.sprite = sprite;
                textQuantity.text    = quantity.ToString();
            }
            else
            {
                productGroup.SetActive(false);
                plusButton.SetActive(true);
            }
        }
    }
}