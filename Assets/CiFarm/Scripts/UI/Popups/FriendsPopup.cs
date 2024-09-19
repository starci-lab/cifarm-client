using System;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.UI.View;
using CiFarm.Scripts.Utilities;
using Imba.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CiFarm.Scripts.UI.Popups
{
    public class FriendsPopup : UIPopup
    {
        [SerializeField]
        private TMP_InputField inputField;

        private UnityAction _onClose;

        protected override void OnInit()
        {
            base.OnInit();
            inputField.onEndEdit.AddListener((value) =>
            {
                DLogger.Log(value, "Search", LogColors.Chartreuse);
                NakamaCommunityService.Instance.SearchAsync(value);
            });
        }

        protected override void OnShowing()
        {
            base.OnShowing();
            if (Parameter != null)
            {
                var param = (GameViewParam)Parameter;
                _onClose = param.callBack;
            }
        }

        protected override void OnHiding()
        {
            base.OnHiding();
            _onClose?.Invoke();
        }

        protected override void OnHidden()
        {
            base.OnHidden();
        }
    }

    [Serializable]
    public class FriendItemData
    {
        public string     userId;
        public string     userName;
        public Sprite     userAva;
        public FriendType type;
    }

    public enum FriendType
    {
        Random,
        Friend,
        Search
    }
}