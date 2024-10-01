using CiFarm.Scripts.Configs.DataClass;
using Imba.Utils;
using Unity.Collections;
using UnityEngine;

namespace CiFarm.Scripts.Configs
{
    public class ConfigManager : ManualSingletonMono<ConfigManager>
    {
        private const string ConfigSharePath = "CsvConfigs/";

        private bool _isLoadedConfigLocal = false;

        #region GAME_CONFIG

       [ReadOnly] public TutorialsDetailConfig TutorialsDetailConfig;
       [ReadOnly] public TutorialsConfig       TutorialsConfig;

        #endregion

        private void Start()
        {
            LoadAllConfigLocal();
        }

        private void LoadAllConfigLocal()
        {
            if (_isLoadedConfigLocal)
                return;

            TutorialsDetailConfig = new();
            TutorialsDetailConfig.LoadFromAssetPath(ConfigSharePath + "TutorialsDetailConfig");

            TutorialsConfig = new();
            TutorialsConfig.LoadFromAssetPath(ConfigSharePath + "TutorialsConfig");

            foreach (var r in TutorialsDetailConfig.Records)
            {
                Debug.Log(r.Type);
            }
            _isLoadedConfigLocal = true;
        }
    }
}