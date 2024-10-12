using Imba.UI;

namespace CiFarm.Scripts.UI.Popups
{
    public class StructuralDetailPopup : UIPopup
    {
        protected override void OnShowing()
        {
            base.OnShowing();
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