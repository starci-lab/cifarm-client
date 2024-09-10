using System.Collections.Generic;
using CiFarm.Scripts.Utilities;
using UnityEngine;

namespace CiFarm.Scripts.SceneController.Game.PlantCore
{
    public class BasePlant : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer plantRender;
        [SerializeField] private List<Sprite> plantStatesRender;
        [SerializeField] private int plantCurrentState = 0;

        public void SetPlantState(int plantState)
        {
            if (plantState > plantStatesRender.Count)
            {
                DLogger.LogError("Invalid plant state", "Plant", LogColors.OrangeRed);
                DLogger.LogError("Invalid plant state", "Plant");
                DLogger.LogError("Invalid plant state");
                return;
            }
            plantCurrentState  = plantState;
            plantRender.sprite = plantStatesRender[plantCurrentState];
        }
        public void ShowRequired()
        {
          
        }
        public void ShowTimer()
        {
          
        }
    }
}