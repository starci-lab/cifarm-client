using System.Collections;
using AYellowpaper.SerializedCollections;
using CiFarm.Scripts.Utilities;
using TMPro;
using UnityEngine;

namespace CiFarm.Scripts.SceneController.Game.PlantCore
{
    public class DirtBubble : MonoBehaviour
    {
        [SerializeField] private GameObject     bubble;
        [SerializeField] private SpriteRenderer iconRender;
        [SerializeField] private TextMeshPro    timerText;
        [SerializeField] private int            timerTotalDisplayTime = 5;

        [SerializeField]
        [SerializedDictionary("Key", "Sprite")]
        private SerializedDictionary<InjectionType, Sprite> spritesConfig;

        private int    _timerCounter;
        private string _tileId;

        public void SetBubble(string tileId, InjectionType type, int timeData = 0)
        {
            _tileId = tileId;
            if (TileBubbleController.Instance.CheckBubble(_tileId))
            {
                SimplePool.Despawn(gameObject);
                return;
            }

            bubble.SetActive(true);
            TileBubbleController.Instance.OnBubbleAppear(_tileId);
            switch (type)
            {
                case InjectionType.None:
                    bubble.SetActive(false);
                    return;
                case InjectionType.Water:
                case InjectionType.Worm:
                case InjectionType.Grass:
                    timerText.SetActive(false);
                    if (spritesConfig.TryGetValue(type, out var data))
                    {
                        iconRender.sprite = data;
                    }
                    else
                    {
                        bubble.SetActive(false);
                    }

                    return;
                case InjectionType.Timer:
                    _timerCounter = timeData;
                    StartCoroutine(StartTimerCounting());
                    break;
            }
        }

        public IEnumerator StartTimerCounting()
        {
            timerText.SetActive(true);

            var timerDisplayLeft = timerTotalDisplayTime;
            while (timerDisplayLeft > 0)
            {
                var minutes = _timerCounter / 60;
                var seconds = _timerCounter % 60;

                timerText.SetText($"{minutes:D2}:{seconds:D2}");
                timerText.SetActive(true);

                yield return new WaitForSeconds(1);
                _timerCounter--;
                timerDisplayLeft--;
            }

            OffBubble();
        }

        public void OffBubble()
        {
            DLogger.Log("REMOVE:" + _tileId);
            TileBubbleController.Instance.OnBubbleDisappear(_tileId);
            SimplePool.Despawn(gameObject);
        }
    }

    public enum InjectionType
    {
        None,
        Water,
        Worm,
        Grass,
        Timer,
    }
}