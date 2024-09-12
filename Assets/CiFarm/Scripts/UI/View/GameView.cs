using System;
using CiFarm.Scripts.SceneController.Game;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.Utilities;
using DG.Tweening;
using Imba.Audio;
using Imba.UI;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI userCoin;
        [SerializeField] private TextMeshProUGUI userNtfCoinA;
        [SerializeField] private TextMeshProUGUI userNtfCoinB;

        private int _currentCoin;

        protected override void OnShown()
        {
            base.OnShown();
            FetchUserCoin();
        }

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
            //GameController.Instance.CameraController.LockCamera();
            AudioManager.Instance.PlaySFX(AudioName.Click3);
            DLogger.Log("In process OnClickShopNft", "MainUI");
        }

        public void OnClickDaily()
        {
            // GameController.Instance.CameraController.LockCamera();
            AudioManager.Instance.PlaySFX(AudioName.Click3);
            DLogger.Log("In process OnClickDaily", "MainUI");
        }

        public void OnClickInventory()
        {
            //GameController.Instance.CameraController.LockCamera();
            AudioManager.Instance.PlaySFX(AudioName.Click3);
            DLogger.Log("In process OnClickInventory", "MainUI");
        }

        public void OnClickFriend()
        {
            //  GameController.Instance.CameraController.LockCamera();
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
            GameController.Instance.CameraController.UnLockCamera();
        }

        #endregion

        #region NAKAMA

        public async void FetchUserCoin()
        {
            var targetCoin = NakamaAssetService.Instance.golds;
            DOTween.To(() => _currentCoin, x => _currentCoin = x, targetCoin, 0.3f)
                .OnUpdate(() => { userCoin.text = _currentCoin.ToString(); });
        }

        #endregion
    }
}