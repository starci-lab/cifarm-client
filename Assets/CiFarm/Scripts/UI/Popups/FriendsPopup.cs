using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.UI.View;
using CiFarm.Scripts.Utilities;
using Imba.UI;
using SuperScrollView;
using System;
using TMPro;
using UnityEngine;

namespace CiFarm.Scripts.UI.Popups
{
    public class FriendsPopup : UIPopup
    {
        [SerializeField]
        private TMP_InputField inputField;

        private void Start()
        {
            inputField.onEndEdit.AddListener((value) =>
            {   
                DLogger.Log(value, "Search", LogColors.Chartreuse);
                NakamaUsersService.Instance.SearchAsync(value);
            });
        }
    }

}
