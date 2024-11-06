using System.Collections.Generic;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.Utilities;
using UnityEngine;

namespace CiFarm.Scripts.SceneController.Game.PlantCore
{
    public class Animal : MonoBehaviour
    {
        [SerializeField] private Transform        animalPosition;
        [SerializeField] private List<GameObject> animalGrowState;

        [ReadOnly]
        public PlacedItem placedData;

        public void Init(PlacedItem placedItem)
        {
            placedData = placedItem;
            DirtBubble bubble;

            if (!placedItem.seedGrowthInfo.isPlanted)
            {
                gameObject.name = "EmptyTile";
            }
            else
            {
                gameObject.name = placedItem.referenceKey + "Tile";
            }

            // show the fully grow
            if (placedData.animalInfo.hasYielded)
            {
                bubble = TileBubbleController.Instance.SpawnBubble(animalPosition.position);
                bubble.SetBubble(placedData.key, InjectionType.TextQuantity,
                    currentQuantity: placedData.animalInfo.harvestQuantityRemaining,
                    maxQuantity: 0);
            }
            else
            {
                // switch (placedItem.animalInfo.currentState)
                // {
                //     // case CurrentState.NeedWater:
                //     //     bubble = TileBubbleController.Instance.SpawnBubble(animalPosition.position);
                //     //     bubble.SetBubble(placedData.key, InjectionType.Water);
                //     //     break;
                //     // case CurrentState.IsWeedy:
                //     //     bubble = TileBubbleController.Instance.SpawnBubble(animalPosition.position);
                //     //     bubble.SetBubble(placedData.key, InjectionType.Grass);
                //     //     break;
                //     // case CurrentState.IsInfested:
                //     //     bubble = TileBubbleController.Instance.SpawnBubble(animalPosition.position);
                //     //     bubble.SetBubble(placedData.key, InjectionType.Worm);
                //     //     break;
                //     //
                //     // default:
                //     //     TileBubbleController.Instance.HideBubble(placedData.key);
                //     //     break;
                // }
            }
        }
    }
}