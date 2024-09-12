using CiFarm.Scripts.SceneController.Game;
using CiFarm.Scripts.Utilities;
using Imba.Audio;
using Imba.UI;
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
            AudioManager.Instance.PlaySFX(AudioName.Click3);
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.ShopPopup, new GameViewParam
            {
                callBack = OnPopupClose
            });
        }

        public void OnClickShopNft()
        {
            GameController.Instance.CameraController.LockCamera();
            AudioManager.Instance.PlaySFX(AudioName.Click3);
            DLogger.Log("In process OnClickShopNft", "MainUI");
        }

        public void OnClickDaily()
        {
            GameController.Instance.CameraController.LockCamera();
            AudioManager.Instance.PlaySFX(AudioName.Click3);
            DLogger.Log("In process OnClickDaily", "MainUI");
        }

        public void OnClickInventory()
        {
            GameController.Instance.CameraController.LockCamera();
            AudioManager.Instance.PlaySFX(AudioName.Click3);
            DLogger.Log("In process OnClickInventory", "MainUI");
        }

        public void OnClickFriend()
        {
            GameController.Instance.CameraController.LockCamera();
            AudioManager.Instance.PlaySFX(AudioName.Click3);
            DLogger.Log("In process OnClickFriend", "MainUI");
        }

        public void OnClickSetting()
        {
            GameController.Instance.CameraController.LockCamera();
            AudioManager.Instance.PlaySFX(AudioName.Click3);
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.SettingPopup, new GameViewParam
            {
                callBack = OnPopupClose
            });
        }

        public void OnPopupClose()
        {
            DLogger.Log("CLOSED POPUP");
            AudioManager.Instance.PlaySFX(AudioName.Close1);
            GameController.Instance.CameraController.UnLockCamera();
        }
        #endregion
    }
}