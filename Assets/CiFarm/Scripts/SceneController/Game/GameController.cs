using System;
using System.Collections;
using System.Collections.Generic;
using CiFarm.Scripts.SceneController.Game.PlantCore;
using CiFarm.Scripts.Services;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.UI.Popups;
using CiFarm.Scripts.UI.View;
using CiFarm.Scripts.UI.View.GameViewComponent;
using CiFarm.Scripts.Utilities;
using Imba.Audio;
using Imba.UI;
using Imba.Utils;
using UnityEngine;

namespace CiFarm.Scripts.SceneController.Game
{
    public class GameController : ManualSingletonMono<GameController>
    {
        [SerializeField] private TileMapController tileMapController;
        [SerializeField] private CameraController  cameraController;

        private List<BaseGround> _baseGrounds;
        private GameView         _gameView;

        private FriendItemData _friendItemData;
        #region GETTET SETTER

        public TileMapController TileMapController => tileMapController;
        public CameraController  CameraController  => cameraController;

        #endregion

        private void Start()
        {
            _baseGrounds = new List<BaseGround>();
            UIManager.Instance.ViewManager.ShowView(UIViewName.GameView);
            _gameView = UIManager.Instance.ViewManager.GetViewByName<GameView>(UIViewName.GameView);
            _gameView.Show();
            LoadUserTileMap();
            NakamaSocketService.Instance.OnFetchPlacedDataFromServer = OnFetchPlacedDataFromServer;
            NakamaCommunityService.Instance.OnVisitUser              = OnVisitUser;

            UIManager.Instance.HideTransition(() => { });
            // SHOW TO UI
            // LoadNormalDirt();
        }

        public void LoadFriendHouse(FriendItemData friendData)
        {
            _friendItemData = friendData;
            OnLoadFriendWithAnimation(friendData.userId);
        }

        #region Nakama Communicated

        #region Loader Data

        /// <summary>
        /// INIT METHOD
        /// </summary>
        private void LoadUserTileMap()
        {
            var rawData = NakamaSocketService.Instance.placedItems;
            foreach (var placed in rawData)
            {
                switch (placed.type)
                {
                    case PlacedItemType.Tile:
                        PlacedDirt(placed);
                        break;
                    case PlacedItemType.Building:
                        break;
                }
            }
        }

        private void PlacedDirt(PlacedItem placedItem)
        {
            var prefabDirtData =
                ResourceService.Instance.ModelGameObjectConfig.GetTileObjectModel(placedItem.referenceKey);
            var dirtObj = SimplePool.Spawn(prefabDirtData, Vector3.zero, prefabDirtData.transform.rotation);
            // var dirtObj = Instantiate(prefabDirtData);

            tileMapController.SetGroundWithTilePos(
                new Vector2Int((int)placedItem.position.x, (int)placedItem.position.y)
                , dirtObj);

            var dirtScript = dirtObj.GetComponent<BaseGround>();
            dirtScript.Init(placedItem);
            if (placedItem.isPlanted)
            {
                var prefabPlantData =
                    ResourceService.Instance.ModelGameObjectConfig.GetPlantObjectModel(placedItem.seedGrowthInfo.seed
                        .key);
                // var plantObj = Instantiate(prefabPlantData);
                var plantObj = SimplePool.Spawn(prefabPlantData, Vector3.zero, prefabPlantData.transform.rotation);

                var plant = plantObj.GetComponent<BasePlant>();
                plant.SetPlantState(placedItem.seedGrowthInfo.currentStage);
                dirtScript.SetPlant(plant);
            }

            _baseGrounds.Add(dirtScript);
        }

        private void PlacedBuilding(PlacedItem placedItem)
        {
        }

        #endregion

        public void OnFetchPlacedDataFromServer()
        {
            DLogger.Log("REALTIME FETCH", "GAME TILE");

            _baseGrounds.Reverse();
            foreach (var dirt in _baseGrounds)
            {
                if (dirt.plant != null)
                {
                    SimplePool.Despawn(dirt.plant.gameObject);
                }

                SimplePool.Despawn(dirt.gameObject);
            }

            _baseGrounds.Clear();

            LoadUserTileMap();
        }

        /// <summary>
        /// Thu hoach
        /// </summary>
        /// <param name="ground"></param>
        public async void OnHarvestPlant(BaseGround ground)
        {
            try
            {
                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
                await NakamaRpcService.Instance.HarvestPlantRpcAsync(new()
                {
                    placedItemTileKey = ground.dirtData.key,
                });

                UIManager.Instance.AlertManager.ShowAlertMessage("Get " +
                                                                 ground.dirtData.seedGrowthInfo
                                                                     .harvestQuantityRemaining +
                                                                 " " + ground.dirtData.seedGrowthInfo.seed.key);
                ground.RemovePlant();
                TileBubbleController.Instance.HideBubble(ground.dirtData.key);
            }
            catch (Exception e)
            {
                DLogger.LogError("Harvest Item error: " + e.Message, "Ground");
            }
        }

        /// <summary>
        /// Tuoi cay
        /// </summary>
        /// <param name="ground"></param>
        public async void OnWaterPlant(BaseGround ground)
        {
            if (_gameView.ToolManager.CurrentTool.toolType != ToolType.WaterCan)
            {
                DLogger.Log("Current tool not water ");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.Watering);
                await NakamaRpcService.Instance.WaterRpcAsync(new()
                {
                    placedItemTileKey = ground.dirtData.key,
                });

                TileBubbleController.Instance.HideBubble(ground.dirtData.key);
            }
            catch (Exception e)
            {
                DLogger.LogError("OnWaterPlant error: " + e.Message, "Ground");
            }
        }

        /// <summary>
        /// Phun thuoc tru sau
        /// </summary>
        /// <param name="ground"></param>
        public async void OnPesticidePlant(BaseGround ground)
        {
            if (_gameView.ToolManager.CurrentTool.toolType != ToolType.Pesticide)
            {
                DLogger.Log("Current tool not Pesticide");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.Spray);
                await NakamaRpcService.Instance.UsePestisideRpcAsync(new()
                {
                    placedItemTileKey = ground.dirtData.key,
                });
                TileBubbleController.Instance.HideBubble(ground.dirtData.key);
            }
            catch (Exception e)
            {
                DLogger.LogError("UsePestisideRpcAsync error: " + e.Message, "Ground");
            }
        }

        /// <summary>
        /// Phun thuoc diet co
        /// </summary>
        /// <param name="ground"></param>
        public async void OnHerbicidePlant(BaseGround ground)
        {
            if (_gameView.ToolManager.CurrentTool.toolType != ToolType.Herbicide)
            {
                DLogger.Log("Current tool not Herbicide");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.Spray);
                await NakamaRpcService.Instance.UseHerbicideRpcAsync(new()
                {
                    placedItemTileKey = ground.dirtData.key
                });

                TileBubbleController.Instance.HideBubble(ground.dirtData.key);
            }
            catch (Exception e)
            {
                DLogger.LogError("UseHerbicideRpcAsync error: " + e.Message, "Ground");
            }
        }

        private void OnLoadFriendWithAnimation(string friendId)
        {
            DLogger.Log("Try visit: " + friendId);
            UIManager.Instance.ShowTransition(() => { NakamaCommunityService.Instance.VisitAsync(friendId); });
        }

        public void OnVisitUser(bool status)
        {
            UIManager.Instance.HideTransition(() => { });
        }

        #endregion
    }
}