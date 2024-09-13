using System.Collections;
using DG.Tweening;
using Imba.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SupernovaDriver.Scripts.SceneController.Entry
{
    public class EntryController : MonoBehaviour
    {
        [SerializeField] private GameObject services;

        [SerializeField] private Image           loaderBar;
        [SerializeField] private TextMeshProUGUI details;

        [SerializeField] private GameObject loadingGroup;
        [SerializeField] private GameObject playButton;

        private string loadingDetais = "Loading";

        private void Awake()
        {
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(services);
        }

        private void Start()
        {
            loaderBar.DOFillAmount(1, Random.Range(2f, 3f)).OnComplete(ShowPlayButton).SetEase(Ease.Linear);
            StartCoroutine(PlayDetailAnimation());
        }

        public IEnumerator PlayDetailAnimation()
        {
            var count = 1;
            while (details.gameObject.activeInHierarchy)
            {
                string dots = new string('.', count);
                details.text = loadingDetais + dots;
                count        = (count % 3) + 1;
                yield return new WaitForSeconds(0.5f);
            }
        }

        public void ShowPlayButton()
        {
            AudioManager.Instance.PlaySFX(AudioName.Click3);

            loadingGroup.SetActive(false);
            playButton.SetActive(true);
        }

        public void LoadGameScene()
        {
            AudioManager.Instance.PlaySFX(AudioName.Click1);
            UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.GameScene);
        }
    }
}