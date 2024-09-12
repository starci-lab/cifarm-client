using CiFarm.Scripts.SceneController.Game;
using CiFarm.Scripts.Utilities;
using Imba.UI;
using UnityEngine;
using UnityEngine.Events;

namespace CiFarm.Scripts.UI.View
{
    public class GameViewParam
    {
        public UnityAction callBack;
    }
    public class GameView : UIView
    {
        #region UI BUTTON

        public void OnClickShop()
        {
            GameController.Instance.CameraController.LockCamera();
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.ShopPopup, new GameViewParam
            {
                callBack = OnPopupClose
            });
        }

        public void OnClickShopNft()
        {
            DLogger.Log("In process OnClickShopNft", "MainUI");
        }

        public void OnClickDaily()
        {
            DLogger.Log("In process OnClickDaily", "MainUI");
        }

        public void OnClickInventory()
        {
            DLogger.Log("In process OnClickInventory", "MainUI");
        }

        public void OnClickFriend()
        {
            DLogger.Log("In process OnClickFriend", "MainUI");
        }

        public void OnClickSetting()
        {
            GameController.Instance.CameraController.LockCamera();
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.SettingPopup, new GameViewParam
            {
                callBack = OnPopupClose
            });
        }

        public void OnPopupClose()
        {
            GameController.Instance.CameraController.UnLockCamera();
        }
        #endregion
    }
}