using CiFarm.Scripts.UI.View;
using Imba.Audio;
using Imba.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace CiFarm.Scripts.UI.Popups
{
    public class SettingPopup : UIPopup
    {
        [SerializeField] TMP_Text     txtNamePopup;
        [SerializeField] GameObject[] btns = new GameObject[2];
        [SerializeField] GameObject   btnClose;

        [SerializeField] GameObject[] btnSound     = new GameObject[2];
        [SerializeField] GameObject[] iconSound    = new GameObject[2];
        [SerializeField] GameObject[] btnMusic     = new GameObject[2];
        [SerializeField] GameObject[] iconMusic    = new GameObject[2];
        private          bool         _isMuteMusic = true;
        private          bool         _isMuteSfx   = true;

        private UnityAction _onClose;

        protected override void OnShowing()
        {
            base.OnShowing();
            //this.isOnSound = AudioSettingService.Instance.GetSoundVolume() == 1 ? true : false;
            //this.isOnMusic = AudioSettingService.Instance.GetMusicVolume() == 1 ? true : false;
            this._isMuteSfx   = AudioManager.Instance.IsMuteAudio(Imba.Audio.AudioType.SFX);
            this._isMuteMusic = AudioManager.Instance.IsMuteAudio(Imba.Audio.AudioType.BGM);
            OnSound(_isMuteSfx);
            OnMusic(_isMuteMusic);

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

        public void BTN_Sound()
        {
            _isMuteSfx = !_isMuteSfx;
            OnSound(_isMuteSfx);
            //PlayerPrefs.SetInt("MuteSFX", _isMuteSfx ? 1 : 0);
            AudioManager.Instance.SetSound(_isMuteSfx);
        }

        public void BTN_Music()
        {
            _isMuteMusic = !_isMuteMusic;
            OnMusic(_isMuteMusic);
            AudioManager.Instance.SetMusic(_isMuteMusic);
            if (_isMuteMusic)
                AudioManager.Instance.StopMusic(AudioName.BGM_Menu);
            else
            {
                if (SceneManager.GetActiveScene().name == "GameScene")
                    AudioManager.Instance.PlayMusic(AudioName.BGM_Menu);
            }
        }

        public void BTN_Home()
        {
            Hide();
        }

        public void OnMusic(bool isMute)
        {
            btnMusic[0].SetActive(!isMute);
            iconMusic[0].SetActive(!isMute);
            btnMusic[1].SetActive(isMute);
            iconMusic[1].SetActive(isMute);
        }

        public void OnSound(bool isMute)
        {
            btnSound[0].SetActive(!isMute);
            iconSound[0].SetActive(!isMute);
            btnSound[1].SetActive(isMute);
            iconSound[1].SetActive(isMute);
        }
    }
}