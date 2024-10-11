using System;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.UI.Popups.Tutorial;
using CiFarm.Scripts.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CiFarm.Scripts.SceneController.Game.PlantCore
{
    public class BaseGround : MonoBehaviour, ITutorialItem
    {
        [SerializeField] private Transform positionPlant;

        [ReadOnly]
        public BasePlant plant;

        [ReadOnly]
        public PlacedItem dirtData;

        public void Init(PlacedItem placedItem)
        {
            dirtData = placedItem;
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
            if (dirtData.seedGrowthInfo.fullyMatured)
            {
                bubble = TileBubbleController.Instance.SpawnBubble(positionPlant.position);
                bubble.SetBubble(dirtData.key, InjectionType.TextQuantity,
                    currentQuantity: dirtData.seedGrowthInfo.harvestQuantityRemaining,
                    maxQuantity: dirtData.seedGrowthInfo.crop.maxHarvestQuantity);
            }
            else
            {
                switch (placedItem.seedGrowthInfo.currentState)
                {
                    case CurrentState.NeedWater:
                        bubble = TileBubbleController.Instance.SpawnBubble(positionPlant.position);
                        bubble.SetBubble(dirtData.key, InjectionType.Water);
                        break;
                    case CurrentState.IsWeedy:
                        bubble = TileBubbleController.Instance.SpawnBubble(positionPlant.position);
                        bubble.SetBubble(dirtData.key, InjectionType.Grass);
                        break;
                    case CurrentState.IsInfested:
                        bubble = TileBubbleController.Instance.SpawnBubble(positionPlant.position);
                        bubble.SetBubble(dirtData.key, InjectionType.Worm);
                        break;
                    default:
                        TileBubbleController.Instance.HideBubble(dirtData.key);
                        break;
                }
            }
        }

        public void SetPlant(BasePlant plantToSet)
        {
            plant = plantToSet;
            plant.transform.SetParent(transform);
            plant.transform.position = positionPlant.position;
        }

        public void RemovePlant()
        {
            SimplePool.Despawn(plant.gameObject);
            dirtData = null;
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            GameController.Instance.OnClickGround(this);
        }

        public void ClearGround()
        {
            SimplePool.Despawn(gameObject);
        }

        public void HandleClickInTutorial(GameObject baseObj)
        {
            GameController.Instance.OnClickGround(baseObj.GetComponent<BaseGround>());
        }

        private void OnDisable()
        {
            if (plant != null)
            {
                SimplePool.Despawn(plant.gameObject);
            }

            plant    = null;
            dirtData = null;
        }
    }
}