using System.Collections.Generic;
using CiFarm.Scripts.Configs;
using CiFarm.Scripts.Configs.DataClass;
using CiFarm.Scripts.UI.Popups;
using CiFarm.Scripts.Utilities;
using Imba.UI;
using Imba.Utils;
using UnityEngine.UI;

namespace CiFarm.Scripts.SceneController.Game
{
    public class TutorialController : ManualSingletonMono<TutorialController>
    {
        public TutorialRecord tutorialRecord;

        public List<TutorialDetailRecord> tutorialDetailRecord;

        private int _currentIndex;

        public override void Awake()
        {
            base.Awake();
            tutorialDetailRecord = new();
        }

        public void StartTutorial(int tutorId)
        {
            LoadTutorial(tutorId);
            ProceedToNextStep();
        }

        public void LoadTutorial(int tutorId)
        {
            _currentIndex  = 0;
            tutorialRecord = ConfigManager.Instance.TutorialsConfig.GetConfigById(tutorId);
            var step = tutorialRecord.GetTutorialDetailsId();
            foreach (var idDetail in step)
            {
                tutorialDetailRecord.Add(ConfigManager.Instance.TutorialsDetailConfig.GetConfigById(idDetail));
            }
        }

        private void ProceedToNextStep()
        {
            if (_currentIndex < tutorialDetailRecord.Count)
            {
                var nextStep = tutorialDetailRecord[_currentIndex];
                HandleTutorialStep(nextStep);
                _currentIndex++;
            }
            else
            {
                EndTutorial();
            }
        }

        public void HandleTutorialStep(TutorialDetailRecord tutorialDetail)
        {
            switch (tutorialDetail.Type)
            {
                case TutorialsDetailType.PopupMessage:
                    ShowPopupMessage(tutorialDetail);
                    break;
                case TutorialsDetailType.ActionClick:
                    HandleActionClick(tutorialDetail);
                    break;
                case TutorialsDetailType.PopupMessageImage:
                    ShowPopupWithImage(tutorialDetail);
                    break;
            }
        }

        private void ShowPopupMessage(TutorialDetailRecord tutorialDetail)
        {
            DLogger.Log("Message tutor: " + tutorialDetail.Details, nameof(TutorialController));
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.CharacterMessagePopup, new CharacterMessageParam
            {
                Type          = TutorialsDetailType.PopupMessage,
                Localization  = tutorialDetail.Localization,
                Details       = tutorialDetail.Details,
                CharacterId   = tutorialDetail.CharacterId,
                TargetImageId = tutorialDetail.TargetImageId,
                OnClose       = ProceedToNextStep
            });
        }

        private void ShowPopupWithImage(TutorialDetailRecord tutorialDetailRecord)
        {
        }

        private void HandleActionClick(TutorialDetailRecord tutorialDetailRecord)
        {
        }

        private void OnActionClickCompleted(Button targetButton)
        {
            ProceedToNextStep();
        }

        public void EndTutorial()
        {
        }
    }
}