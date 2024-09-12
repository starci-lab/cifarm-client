using CiFarm.Scripts.UI.View;
using Imba.Audio;
using Imba.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CiFarm.Scripts.UI.Popups
{
    public class SettingPopup : UIPopup
    {
        [SerializeField] private Slider musicSetting;
        [SerializeField] private Slider audiSetting;

        private UnityAction _onClose;

        protected override void OnShowing()
        {
            base.OnShowing();

            musicSetting.SetValueWithoutNotify(AudioManager.Instance._musicVolume);
            audiSetting.SetValueWithoutNotify(AudioManager.Instance._audioVolume);
            if (Parameter != null)
            {
                var param = (GameViewParam)Parameter;
                _onClose = param.callBack;
            }
        }

        protected override void OnHiding()
        {
            base.OnHiding();
            AudioManager.Instance.SaveAudioSetting();
            _onClose?.Invoke();
        }

        public void BTN_Home()
        {
            Hide();
        }

        public void OnChangeMusic(float value)
        {
            AudioManager.Instance.SetMusicVolume(value);
        }

        public void OnChangeAudio(float value)
        {
            AudioManager.Instance.SetAudioVolume(value);
        }
        public void OnDoneEdit()
        {
            AudioManager.Instance.PlaySFX(AudioName.Click4);

        }
    }
}