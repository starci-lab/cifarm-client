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

        
        public void BTN_Home()
        {
            Hide();
        }

       
    }
}