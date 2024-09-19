using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private VisitView        _visitView;

        private FriendItemData _friendItemData;

        #region GETTET SETTER

        public TileMapController TileMapController => tileMapController;
        public CameraController  CameraController  => cameraController;

        #endregion

        public override void Awake()
        {
            base.Awake();
            _baseGrounds = new List<BaseGround>();
            _gameView    = UIManager.Instance.ViewManager.GetViewByName<GameView>(UIViewName.GameView);
            _visitView   = UIManager.Instance.ViewManager.GetViewByName<VisitView>(UIViewName.VisitView);
        }

        private void Start()
        {
            UIManager.Instance.ViewManager.ShowView(UIViewName.GameView);
            _gameView.Show();

            LoadUserTileMap();

            NakamaSocketService.Instance.OnFetchPlacedDataFromServer = OnFetchPlacedDataFromServer;
            NakamaCommunityService.Instance.OnVisitUser              = OnVisitUser;
            NakamaCommunityService.Instance.OnReturn                 = OnReturnHome;

            UIManager.Instance.HideTransition(() => { });
        }

        public void OnClickGround(BaseGround clickedGround)
        {
            if (_friendItemData == null)
            {
                HandleClickMyGround(clickedGround);
            }
            else
            {
                HandleClickOtherGround(clickedGround);
            }
        }

        public void HandleClickMyGround(BaseGround clickedGround)
        {
            // Planting
            if (!clickedGround.dirtData.isPlanted)
            {
                UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PlantingPopup, new CustomInventoryPopupParam
                {
                    PlantingPopupType = PlantingPopupType.Planting,
                    CloseAction       = null,
                    PlantAction       = clickedGround.OnConfirmSetPlant
                });
                return;
            }

            // Harvest
            if (clickedGround.dirtData.isPlanted && clickedGround.dirtData.fullyMatured)
            {
                OnHarvestPlant(clickedGround);
                return;
            }

            // Planted and everything normal
            if (clickedGround.dirtData.isPlanted &&
                clickedGround.dirtData.seedGrowthInfo.plantCurrentState == PlantCurrentState.Normal)
            {
                var bubble = clickedGround.SpawnBubble();
                bubble.SetBubble(clickedGround.dirtData.key, InjectionType.Timer,
                    clickedGround.dirtData.seedGrowthInfo.crop.growthStageDuration -
                    (int)clickedGround.dirtData.seedGrowthInfo.currentStageTimeElapsed);
                return;
            }

            // REQUIRED SOME THING
            if (clickedGround.dirtData.isPlanted &&
                clickedGround.dirtData.seedGrowthInfo.plantCurrentState != PlantCurrentState.Normal)
            {
                switch (clickedGround.dirtData.seedGrowthInfo.plantCurrentState)
                {
                    case PlantCurrentState.NeedWater:
                        OnWaterPlant(clickedGround);
                        break;
                    case PlantCurrentState.IsWeedy:
                        OnHerbicidePlant(clickedGround);
                        break;
                    case PlantCurrentState.IsInfested:
                        OnPesticidePlant(clickedGround);
                        break;
                }

                return;
            }
        }

        public void HandleClickOtherGround(BaseGround clickedGround)
        {
            // An trom
            if (clickedGround.dirtData.isPlanted && clickedGround.dirtData.fullyMatured)
            {
                OnHandOfMidasPlant(clickedGround);
                return;
            }

            // Planted and everything normal
            if (clickedGround.dirtData.isPlanted &&
                clickedGround.dirtData.seedGrowthInfo.plantCurrentState == PlantCurrentState.Normal)
            {
                var bubble = clickedGround.SpawnBubble();
                bubble.SetBubble(clickedGround.dirtData.key, InjectionType.Timer,
                    clickedGround.dirtData.seedGrowthInfo.crop.growthStageDuration -
                    (int)clickedGround.dirtData.seedGrowthInfo.currentStageTimeElapsed);
                return;
            }

            // REQUIRED SOMETHING
            if (clickedGround.dirtData.isPlanted &&
                clickedGround.dirtData.seedGrowthInfo.plantCurrentState != PlantCurrentState.Normal)
            {
                switch (clickedGround.dirtData.seedGrowthInfo.plantCurrentState)
                {
                    case PlantCurrentState.NeedWater:
                        Instance.OnHelpWaterPlant(clickedGround);
                        break;
                    case PlantCurrentState.IsWeedy:
                        Instance.OnHelpHerbicidePlant(clickedGround);
                        break;
                    case PlantCurrentState.IsInfested:
                        Instance.OnHelpPesticidePlant(clickedGround);
                        break;
                }

                return;
            }
        }

        public void LoadFriendHouse(FriendItemData friendData)
        {
            _friendItemData = friendData;
            LoadFriendWithAnimation(friendData.userId);
        }

        public void ReturnHome()
        {
            _friendItemData = null;
            LoadHomeWithAnimation();
        }

        public void OnVisitUser(bool status)
        {
            if (status)
            {
                _gameView.Hide();
                _visitView.Show(new VisitViewParam
                {
                    userName         = _friendItemData.userName,
                    userLevel        = 1,
                    userLevelProcess = 0.7f,
                    userAva          = null
                });
            }

            UIManager.Instance.HideTransition(() =>
            {
                if (!status)
                {
                    UIManager.Instance.PopupManager.ShowMessageDialog("Error", "Visit fail...");
                }
            });
        }

        public void OnReturnHome(bool status)
        {
            TileBubbleController.Instance.ClearAllBubble();
            OnFetchPlacedDataFromServer();

            if (status)
            {
                _gameView.Show();
                _visitView.Hide();
            }

            UIManager.Instance.HideTransition(() =>
            {
                if (!status)
                {
                    UIManager.Instance.PopupManager.ShowMessageDialog("Error", "Return fail...");
                }
            });
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

            DLogger.Log("VALIDATE " + rawData.Select(o => o.key).ToList());
            TileBubbleController.Instance.ValidateBubble(
                rawData.Select(o => o.key).ToList());
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
                    ResourceService.Instance.ModelGameObjectConfig.GetPlantObjectModel(placedItem.seedGrowthInfo.crop
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
            // DLogger.Log("REALTIME FETCH", "GAME TILE");

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

        #region My Ground Manage

        /// <summary>
        /// Thu hoach
        /// </summary>
        /// <param name="ground"></param>
        public async void OnHarvestPlant(BaseGround ground)
        {
            try
            {
                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
                await NakamaRpcService.Instance.HarvestCropRpcAsync(new()
                {
                    placedItemTileKey = ground.dirtData.key,
                });

                UIManager.Instance.AlertManager.ShowAlertMessage("Get " +
                                                                 ground.dirtData.seedGrowthInfo
                                                                     .harvestQuantityRemaining +
                                                                 " " + ground.dirtData.seedGrowthInfo.crop.key);
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

        #endregion

        #region FriendZone

        private void LoadFriendWithAnimation(string friendId)
        {
            DLogger.Log("Try visit: " + friendId);
            UIManager.Instance.ShowTransition(() => { NakamaCommunityService.Instance.VisitAsync(friendId); });
        }

        private void LoadHomeWithAnimation()
        {
            UIManager.Instance.ShowTransition(() => { NakamaCommunityService.Instance.ReturnAsync(); });
        }

        /// <summary>
        /// An trom
        /// </summary>
        /// <param name="ground"></param>
        public async void OnHandOfMidasPlant(BaseGround ground)
        {
            try
            {
                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
                await NakamaRpcService.Instance.ThiefCropRpcAsync(new()
                {
                    userId            = _friendItemData.userId,
                    placedItemTileKey = ground.dirtData.key,
                });

                UIManager.Instance.AlertManager.ShowAlertMessage("Get " + 1 + " " +
                                                                 ground.dirtData.seedGrowthInfo.crop.key);

                TileBubbleController.Instance.HideBubble(ground.dirtData.key);
            }
            catch (Exception e)
            {
                DLogger.LogError("Steal Item error: " + e.Message, "Ground");
            }
        }

        /// <summary>
        /// Tuoi cay
        /// </summary>
        /// <param name="ground"></param>
        public async void OnHelpWaterPlant(BaseGround ground)
        {
            if (_visitView.ToolManager.CurrentTool.toolType != ToolType.WaterCan)
            {
                DLogger.Log("Current tool not water ");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.Watering);
                await NakamaRpcService.Instance.HelpWaterRpcAsync(new()
                {
                    userId            = _friendItemData.userId,
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
        public async void OnHelpPesticidePlant(BaseGround ground)
        {
            if (_visitView.ToolManager.CurrentTool.toolType != ToolType.Pesticide)
            {
                DLogger.Log("Current tool not Pesticide");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.Spray);
                await NakamaRpcService.Instance.HelpUsePestisideRpcAsync(new()
                {
                    userId            = _friendItemData.userId,
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
        public async void OnHelpHerbicidePlant(BaseGround ground)
        {
            if (_visitView.ToolManager.CurrentTool.toolType != ToolType.Herbicide)
            {
                DLogger.Log("Current tool not Herbicide");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.Spray);
                await NakamaRpcService.Instance.HelpUseHerbicideRpcAsync(new()
                {
                    userId            = _friendItemData.userId,
                    placedItemTileKey = ground.dirtData.key
                });

                TileBubbleController.Instance.HideBubble(ground.dirtData.key);
            }
            catch (Exception e)
            {
                DLogger.LogError("UseHerbicideRpcAsync error: " + e.Message, "Ground");
            }
        }

        #endregion

        #endregion
    }
}