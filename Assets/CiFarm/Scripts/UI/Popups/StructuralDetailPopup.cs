using CiFarm.Scripts.Services;
using Imba.UI;
using TMPro;
using UnityEngine;

namespace CiFarm.Scripts.UI.Popups
{
    public class StructuralDetailParam
    {
        public string StructuralId;
    }

    public class StructuralDetailPopup : UIPopup
    {
        public string structuralId;

        [SerializeField] private TextMeshProUGUI structuralName;

        protected override void OnShowing()
        {
            base.OnShowing();
            if (Parameter != null)
            {
                var param = (StructuralDetailParam)Parameter;
                structuralId = param.StructuralId;
            }
            
            var gameConfig = ResourceService.Instance.ModelGameObjectConfig.GetTile(structuralId);
            var detail     = ResourceService.Instance.ItemDetailConfig.GetItemDetail(structuralId);

            structuralName.text = detail!.ItemName;
        }

        public void OnClickAddAnimal()
        {
            Hide(true);
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.ShopPopup, new ShopPopupParam
            {
                TabToOpen = 0,
            });
        }
    }
}