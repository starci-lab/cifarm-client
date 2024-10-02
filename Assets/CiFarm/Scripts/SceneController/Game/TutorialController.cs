using System;
using System.Collections.Generic;
using CiFarm.Scripts.Configs;
using CiFarm.Scripts.Configs.DataClass;
using Imba.Utils;
using UnityEngine.UI;

namespace CiFarm.Scripts.SceneController.Game
{
    public class TutorialController : ManualSingletonMono<TutorialController>
    {
        public TutorialRecord tutorialRecord;

        public List<int>                  tutorialDetailStep;
        public List<TutorialDetailRecord> tutorialDetailRecord;

        public override void Awake()
        {
            base.Awake();
            tutorialDetailStep   = new();
            tutorialDetailRecord = new();
        }

        public void StartTutorial(int tutorId)
        {
            LoadTutorial(tutorId);
        }

        public void LoadTutorial(int tutorId)
        {
            tutorialRecord = ConfigManager.Instance.TutorialsConfig.GetConfigById(tutorId);
            var step = tutorialRecord.GetTutorialDetailsId();
            foreach (var idDetail in step)
            {
                tutorialDetailRecord.Add(ConfigManager.Instance.TutorialsDetailConfig.GetConfigById(idDetail));
            }
        }

        public void HandleTutorialStep(TutorialDetailRecord tutorialDetailRecord)
        {
            switch (tutorialDetailRecord.Type)
            {
                case TutorialsDetailType.PopupMessage:
                    ShowPopupMessage();
                    break;
                case TutorialsDetailType.ActionClick:
                    HandleActionClick();
                    break;
                case TutorialsDetailType.PopupMessageImage:
                    ShowPopupWithImage();
                    break;
            }
        }

        private void ShowPopupMessage()
        {
        }

        private void ShowPopupWithImage()
        {
        }

        private void HandleActionClick()
        {
        }

        private void OnActionClickCompleted(Button targetButton)
        {
            ProceedToNextStep();
        }

        private void ProceedToNextStep()
        {
            if (tutorialDetailStep.Count > 0)
            {
                var nextStepId = tutorialDetailStep[0];
                tutorialDetailStep.RemoveAt(0);
                var nextStep = tutorialDetailRecord.Find(record => record.Id == nextStepId);
                HandleTutorialStep(nextStep);
            }
            else
            {
                OnTutorialEnd();
            }
        }

        public void OnTutorialEnd()
        {
        }
    }
}