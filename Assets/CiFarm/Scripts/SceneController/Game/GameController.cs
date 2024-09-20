using System;
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
        [SerializeField] private TileMapController  tileMapController;
        [SerializeField] private CameraController   cameraController;
        [SerializeField] private EditModeController editModeController;

        private List<BaseGround> _baseGrounds;
        private GameView         _gameView;
        private VisitView        _visitView;
        private EditView         _editView;

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
            _editView    = UIManager.Instance.ViewManager.GetViewByName<EditView>(UIViewName.EditView);
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
            // Not init ground
            DLogger.Log("CLICKED " + clickedGround.dirtData.ToString());
            if (string.IsNullOrEmpty(clickedGround.dirtData.key))
            {
                return;
            }

            // Planting
            if (!clickedGround.dirtData.isPlanted)
            {
                UIManager.Instance.PopupManager.ShowPopup(UIPopupName.PlantingPopup, new CustomInventoryPopupParam
                {
                    PlantingPopupType = PlantingPopupType.Planting,
                    CloseAction       = null,
                    PlantAction       = data => { OnPlantSeed(clickedGround, data); }
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
                var bubble = TileBubbleController.Instance.SpawnBubble(clickedGround.transform.position);
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
                var bubble = TileBubbleController.Instance.SpawnBubble(clickedGround.transform.position);
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

        public void EnterEditMode(InvenItemData data)
        {
            // Stop Realtime
            NakamaSocketService.Instance.OnFetchPlacedDataFromServer = null;

            _gameView.Hide();
            _editView.Show(new EditViewParameter
            {
                InventoryId = data.referenceKey
            });

            editModeController.SetUpEditMode(data);
        }

        public void ExitEditMode()
        {
            NakamaSocketService.Instance.OnFetchPlacedDataFromServer = OnFetchPlacedDataFromServer;
            _gameView.Show();
            _editView.Hide();
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

        #region Loader Init

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
                var plantObj = SimplePool.Spawn(prefabPlantData, Vector3.zero, Quaternion.identity);
                var plant    = plantObj.GetComponent<BasePlant>();
                plant.SetPlantState(placedItem.seedGrowthInfo.currentStage);
                dirtScript.SetPlant(plant);
            }

            _baseGrounds.Add(dirtScript);
        }

        private void PlacedBuilding(PlacedItem placedItem)
        {
        }

        public void OnFetchPlacedDataFromServer()
        {
            // DLogger.Log("REALTIME FETCH", "GAME TILE");

            _baseGrounds.Reverse();
            foreach (var dirt in _baseGrounds)
            {
                dirt.ClearGround();
            }

            _baseGrounds.Clear();

            LoadUserTileMap();
        }

        #endregion

        #region My Ground Manage

        /// <summary>
        /// Plants a seed asynchronously using NakamaFarmingService.
        /// </summary>
        /// <param name="ground"></param>
        public async void OnPlantSeed(BaseGround ground, InvenItemData plantData)
        {
            try
            {
                await NakamaFarmingService.Instance.PlantSeedAsync(plantData.key, ground.dirtData.key);
                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
            }
            catch (Exception e)
            {
                DLogger.LogError("Plant Seed error: " + e.Message, "Ground");
            }
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
                await NakamaFarmingService.Instance.HarvestCropAsync(ground.dirtData.key);

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
                DLogger.Log("Current tool is not Water Can.");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.Watering);
                await NakamaFarmingService.Instance.WaterAsync(ground.dirtData.key);
                TileBubbleController.Instance.HideBubble(ground.dirtData.key);
            }
            catch (Exception e)
            {
                DLogger.LogError("OnWaterPlantAsync error: " + e.Message, "Ground");
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
                DLogger.Log("Current tool is not Pesticide.");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.Spray);
                await NakamaFarmingService.Instance.UsePesticideAsync(ground.dirtData.key);
                TileBubbleController.Instance.HideBubble(ground.dirtData.key);
            }
            catch (Exception e)
            {
                DLogger.LogError("OnPesticidePlantAsync error: " + e.Message, "Ground");
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
                DLogger.Log("Current tool is not Herbicide.");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.Spray);
                await NakamaFarmingService.Instance.UseHerbicideAsync(ground.dirtData.key);
                TileBubbleController.Instance.HideBubble(ground.dirtData.key);
            }
            catch (Exception e)
            {
                DLogger.LogError("OnHerbicidePlantAsync error: " + e.Message, "Ground");
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
                await NakamaFarmingService.Instance.ThiefCropAsync(_friendItemData.userId, ground.dirtData.key);

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
                DLogger.Log("Current tool is not Water Can.");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.Watering);
                await NakamaFarmingService.Instance.HelpWaterAsync(_friendItemData.userId, ground.dirtData.key);
                TileBubbleController.Instance.HideBubble(ground.dirtData.key);
            }
            catch (Exception e)
            {
                DLogger.LogError("OnHelpWaterPlantAsync error: " + e.Message, "Ground");
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
                DLogger.Log("Current tool is not Pesticide.");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.Spray);
                await NakamaFarmingService.Instance.HelpUsePesticideAsync(_friendItemData.userId, ground.dirtData.key);
                TileBubbleController.Instance.HideBubble(ground.dirtData.key);
            }
            catch (Exception e)
            {
                DLogger.LogError("OnHelpPesticidePlantAsync error: " + e.Message, "Ground");
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
                DLogger.Log("Current tool is not Herbicide.");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.Spray);
                await NakamaFarmingService.Instance.HelpUseHerbicideAsync(_friendItemData.userId, ground.dirtData.key);
                TileBubbleController.Instance.HideBubble(ground.dirtData.key);
            }
            catch (Exception e)
            {
                DLogger.LogError("OnHelpHerbicidePlantAsync error: " + e.Message, "Ground");
            }
        }

        #endregion

        #endregion
    }
}