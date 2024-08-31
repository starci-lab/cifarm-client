using DuckSurvivor.Scripts.Configs;
using UnityEngine;

namespace SupernovaDriver.Scripts.SceneController.Entry
{
    public class EntryController : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject services;

		private void Awake()
        {
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(services);
		}

        private void Start()
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.MainScene);
        }
    }
}