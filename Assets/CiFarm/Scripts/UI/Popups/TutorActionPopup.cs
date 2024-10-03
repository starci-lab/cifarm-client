using CiFarm.Scripts.Configs.DataClass;
using CiFarm.Scripts.Utilities;
using Imba.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CiFarm.Scripts.UI.Popups
{
    public class TutorActionPopup : UIPopup
    {
        [SerializeField] private TextMeshProUGUI detailStepText;
        [SerializeField] private Transform       fakeButtonContainer;

        private UIButton               _fakeTargetButton;
        private TutorButtonActionParam _tutorParam;
        private UnityAction            _onClose;

        protected override void OnShown()
        {
            base.OnShown();
            if (Parameter == null)
            {
                DLogger.LogWarning("TutorButtonActionPopup open without param, close instead",
                    "TutorButtonActionPopup");
                Hide(true);
                return;
            }

            _tutorParam = (TutorButtonActionParam)Parameter;
            _onClose    = _tutorParam.OnClose;

            detailStepText.text = _tutorParam.Details;
            _fakeTargetButton   = SetUpButton();
        }

        private UIButton SetUpButton()
        {
            GameObject targetButtonObj = GameObject.Find(_tutorParam.TargetClickId);
            if (targetButtonObj == null)
            {
                DLogger.LogWarning($"Target button with ID {_tutorParam.TargetClickId} not found.",
                    "TutorButtonActionPopup");
                Hide(true);
                return null;
            }

            var targetButton = targetButtonObj.GetComponent<UIButton>();

            if (targetButton == null)
            {
                DLogger.LogWarning($"Object with ID {_tutorParam.TargetClickId} does not have a Button component.",
                    "TutorButtonActionPopup");
                Hide(true);
                return null;
            }

            ClearButton();

            _fakeTargetButton = Instantiate(targetButton, fakeButtonContainer);

            _fakeTargetButton.transform.position = targetButtonObj.transform.position;


            // Add the click listener to the fake button
            _fakeTargetButton.OnClick.OnTrigger.Event.AddListener(OnClickReqButton);

            return _fakeTargetButton;
        }

        private void ClearButton()
        {
            if (_fakeTargetButton != null)
            {
                Destroy(_fakeTargetButton.gameObject);
            }
        }

        public void OnClickReqButton()
        {
            ClearButton();
            Hide(true);
            _onClose?.Invoke();
        }
    }

    public class TutorButtonActionParam
    {
        public TutorialsDetailType Type;
        public string              Localization;
        public string              Details;
        public string              TargetClickId;
        public UnityAction         OnClose;
    }
}