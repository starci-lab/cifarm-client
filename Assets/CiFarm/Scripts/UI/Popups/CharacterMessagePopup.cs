using CiFarm.Scripts.Configs.DataClass;
using CiFarm.Scripts.Utilities;
using Imba.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CiFarm.Scripts.UI.Popups
{
    public class CharacterMessagePopup : UIPopup
    {
        [SerializeField] private TextMeshProUGUI dialogMessage;
        [SerializeField] private Transform       characterModelContainer;

        private CharacterMessageParam _characterMessageParam;
        private UnityAction           _onClose;

        private GameObject _characterObject;

        private void Start()
        {
            if (Parameter == null)
            {
                DLogger.LogWarning("CharacterMessagePopup open without param, close instead", "CharacterMessagePopup");
                Hide(true);
                return;
            }

            _characterMessageParam = (CharacterMessageParam)Parameter;
            _onClose               = _characterMessageParam.OnClose;
            dialogMessage.text     = _characterMessageParam.Details;
            LoadCharacter();
        }

        public void Close()
        {
            if (_characterObject)
            {
                Destroy(_characterObject);
            }

            Hide(true);
            _onClose?.Invoke();
        }

        private void LoadCharacter()
        {
            var charModel = Resources.Load<GameObject>("CharacterModel/" + _characterMessageParam.CharacterId);
            if (_characterObject)
            {
                Destroy(_characterObject);
            }

            _characterObject = Instantiate(charModel, characterModelContainer);
        }
    }

    public class CharacterMessageParam
    {
        public TutorialsDetailType Type;
        public string              Localization;
        public string              Details;
        public string              CharacterId;
        public string              TargetImageId;
        public UnityAction         OnClose;
    }
}