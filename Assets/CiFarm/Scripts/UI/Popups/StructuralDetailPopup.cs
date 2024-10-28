using CiFarm.Scripts.Services;
using Imba.UI;
using TMPro;
using UnityEngine;

namespace CiFarm.Scripts.UI.Popups
{
    public class StructuralDetailParam
    {
        public string StructuralId;
        public string ReferenceId;
    }

    public class StructuralDetailPopup : UIPopup
    {
        public string structuralId;
        public string referenceId;

        [SerializeField] private TextMeshProUGUI structuralName;

        protected override void OnShowing()
        {
            base.OnShowing();
            if (Parameter != null)
            {
                var param = (StructuralDetailParam)Parameter;
                structuralId = param.StructuralId;
                referenceId  = param.ReferenceId;
            }

            var gameConfig = ResourceService.Instance.ModelGameObjectConfig.GetTile(referenceId);
            var detail     = ResourceService.Instance.ItemDetailConfig.GetItemDetail(referenceId);

            structuralName.text = detail!.ItemName;
        }

        public void OnClickAddAnimal()
        {
            Hide(true);
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.ShopPopup, new ShopPopupParam
            {
                TabToOpen = ShopType.Animal,
                HideOther = true,
            });
        }
    }
}