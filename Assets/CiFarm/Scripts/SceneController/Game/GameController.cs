using System;
using System.Collections.Generic;
using System.Linq;
using CiFarm.Scripts.SceneController.Game.PlantCore;
using CiFarm.Scripts.Services;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.Services.NakamaServices.NakamaRawService;
using CiFarm.Scripts.UI.Popups;
using CiFarm.Scripts.UI.View;
using CiFarm.Scripts.UI.View.GameViewComponent;
using CiFarm.Scripts.Utilities;
using CiFarm.Scripts.Vfx;
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
        private List<GameObject> _constructor;
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
            _baseGrounds = new();
            _constructor = new();
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

            if (TutorialController.Instance.AvailableTutorCheck())
            {
                TutorialController.Instance.StartTutorial();
            }
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.A))
            {
                UIManager.Instance.PopupManager.ShowPopup(UIPopupName.CharacterMessagePopup);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                UIManager.Instance.PopupManager.HidePopup(UIPopupName.CharacterMessagePopup);
            }
#endif
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
            if (string.IsNullOrEmpty(clickedGround.dirtData.key))
            {
                return;
            }

            // Planting
            if (!clickedGround.dirtData.seedGrowthInfo.isPlanted)
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
            if (clickedGround.dirtData.seedGrowthInfo.isPlanted && clickedGround.dirtData.seedGrowthInfo.fullyMatured)
            {
                OnHarvestPlant(clickedGround);
                return;
            }

            // Planted and everything normal
            if (clickedGround.dirtData.seedGrowthInfo.isPlanted &&
                clickedGround.dirtData.seedGrowthInfo.plantCurrentState == PlantCurrentState.Normal)
            {
                var bubble = TileBubbleController.Instance.SpawnBubble(clickedGround.transform.position);
                bubble.SetBubble(clickedGround.dirtData.key, InjectionType.Timer,
                    (int)clickedGround.dirtData.seedGrowthInfo.crop.growthStageDuration -
                    (int)clickedGround.dirtData.seedGrowthInfo.currentStageTimeElapsed);
                return;
            }

            // REQUIRED SOME THING
            if (clickedGround.dirtData.seedGrowthInfo.isPlanted &&
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
            Debug.Log("Try Clicked 3");
            if (clickedGround.dirtData.seedGrowthInfo.isPlanted && clickedGround.dirtData.seedGrowthInfo.fullyMatured)
            {
                OnHandOfMidasPlant(clickedGround);
                return;
            }

            // Planted and everything normal
            if (clickedGround.dirtData.seedGrowthInfo.isPlanted &&
                clickedGround.dirtData.seedGrowthInfo.plantCurrentState == PlantCurrentState.Normal)
            {
                var bubble = TileBubbleController.Instance.SpawnBubble(clickedGround.transform.position);
                bubble.SetBubble(clickedGround.dirtData.key, InjectionType.Timer,
                    (int)clickedGround.dirtData.seedGrowthInfo.crop.growthStageDuration -
                    (int)clickedGround.dirtData.seedGrowthInfo.currentStageTimeElapsed);
                return;
            }

            // REQUIRED SOMETHING
            if (clickedGround.dirtData.seedGrowthInfo.isPlanted &&
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

            editModeController.EnterEditMode(data);
        }

        public void ExitEditMode()
        {
            NakamaSocketService.Instance.OnFetchPlacedDataFromServer = OnFetchPlacedDataFromServer;
            _gameView.Show();
            _editView.Hide();

            editModeController.ExitEditMode();
        }

        public async void OnVisitUser(bool status)
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

            await NakamaSocketService.Instance.ForceCentralBroadcastInstantlyRpcAsync();
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
            TileMapController.ResetPosition();
            var rawData = NakamaSocketService.Instance.placedItems;
            foreach (var placed in rawData)
            {
                switch (placed.type)
                {
                    case PlacedItemType.Tile:
                        PlacedDirt(placed);
                        break;
                    case PlacedItemType.Building:
                        PlacedBuilding(placed);
                        break;
                }
            }

            TileBubbleController.Instance.ValidateBubble(
                rawData.Select(o => o.key).ToList());
        }

        private void PlacedDirt(PlacedItem placedItem)
        {
            var prefabDirtData =
                ResourceService.Instance.ModelGameObjectConfig.GetTileObjectModel(placedItem.referenceKey);
            var dirtObj = SimplePool.Spawn(prefabDirtData, Vector3.zero, prefabDirtData.transform.rotation);
            // var dirtObj = Instantiate(prefabDirtData);

            tileMapController.SetAnyWithWithTilePos(
                new Vector2Int(placedItem.position.x, placedItem.position.y)
                , dirtObj);

            var dirtScript = dirtObj.GetComponent<BaseGround>();
            dirtScript.Init(placedItem);
            if (placedItem.seedGrowthInfo.isPlanted)
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
            var tileObjectModel =
                ResourceService.Instance.ModelGameObjectConfig.GetTileObjectModel(placedItem.referenceKey);
            var tileObject = SimplePool.Spawn(tileObjectModel, Vector3.zero, tileObjectModel.transform.rotation);

            tileMapController.SetAnyWithWithTilePos(
                new Vector2Int(placedItem.position.x, placedItem.position.y)
                , tileObject);
            
            _constructor.Add(tileObject);
        }

        private void OnFetchPlacedDataFromServer()
        {
            // DLogger.Log("REALTIME FETCH", "GAME TILE");

            _baseGrounds.Reverse();
            foreach (var dirt in _baseGrounds)
            {
                dirt.ClearGround();
            }

            foreach (var construct in _constructor)
            {
                SimplePool.Despawn(construct);
            }

            _baseGrounds.Clear();
            _constructor.Clear();

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
            if (_gameView.ToolManager.CurrentTool.toolType != ToolType.Scythe)
            {
                DLogger.Log("Current tool is not Scythe.");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
                await NakamaFarmingService.Instance.HarvestCropAsync(ground.dirtData.key);

                PlayHarvestEf(ground.transform.position, ground.dirtData.seedGrowthInfo.crop.key, ground.dirtData
                    .seedGrowthInfo
                    .harvestQuantityRemaining);
                PlayExperiencesEf(ground.transform.position,
                    ground.dirtData.seedGrowthInfo.crop.basicHarvestExperiences);

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
                await NakamaFarmingService.Instance.WaterAsync(ground.dirtData.key);
                EffectService.Instance.PlayVFX(VFXType.WaterCan, ground.transform.position, 1f);
                PlayExperiencesEf(ground.transform.position, ExperienceConstants.ExperienceFromActivity);
                AudioManager.Instance.PlaySFX(AudioName.Watering);
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
                await NakamaFarmingService.Instance.UsePesticideAsync(ground.dirtData.key);
                EffectService.Instance.PlayVFX(VFXType.Pesticide, ground.transform.position, 1f);
                PlayExperiencesEf(ground.transform.position, ExperienceConstants.ExperienceFromActivity);
                AudioManager.Instance.PlaySFX(AudioName.Spray);
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
                await NakamaFarmingService.Instance.UseHerbicideAsync(ground.dirtData.key);
                EffectService.Instance.PlayVFX(VFXType.Herbicide, ground.transform.position, 1f);
                PlayExperiencesEf(ground.transform.position, ExperienceConstants.ExperienceFromActivity);
                AudioManager.Instance.PlaySFX(AudioName.Spray);

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
            if (_visitView.ToolManager.CurrentTool.toolType != ToolType.Steal)
            {
                DLogger.Log("Current tool is not Steal.");
                return;
            }

            if (ground.dirtData.seedGrowthInfo.thiefedBy != null &&
                ground.dirtData.seedGrowthInfo.thiefedBy.Contains(NakamaUserService.Instance.userId))
            {
                UIManager.Instance.AlertManager.ShowAlertMessage("You have already stolen this crop.");
                return;
            }

            if (ground.dirtData.seedGrowthInfo.harvestQuantityRemaining ==
                ground.dirtData.seedGrowthInfo.crop.minHarvestQuantity)
            {
                UIManager.Instance.AlertManager.ShowAlertMessage("This crop has reached the minimum harvest quantity");
                return;
            }

            try
            {
                AudioManager.Instance.PlaySFX(AudioName.PowerUpBright);
                await NakamaFarmingService.Instance.ThiefCropAsync(_friendItemData.userId, ground.dirtData.key);
                PlayHarvestEf(ground.transform.position, ground.dirtData.seedGrowthInfo.crop.key, 1);
                PlayExperiencesEf(ground.transform.position, ExperienceConstants.ExperienceFromActivity);

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
                await NakamaFarmingService.Instance.HelpWaterAsync(_friendItemData.userId, ground.dirtData.key);
                EffectService.Instance.PlayVFX(VFXType.WaterCan, ground.transform.position, 1f);
                PlayExperiencesEf(ground.transform.position, ExperienceConstants.ExperienceFromActivity);
                AudioManager.Instance.PlaySFX(AudioName.Watering);
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
                await NakamaFarmingService.Instance.HelpUsePesticideAsync(_friendItemData.userId, ground.dirtData.key);
                EffectService.Instance.PlayVFX(VFXType.Pesticide, ground.transform.position, 1f);
                PlayExperiencesEf(ground.transform.position, ExperienceConstants.ExperienceFromActivity);
                AudioManager.Instance.PlaySFX(AudioName.Spray);
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
                await NakamaFarmingService.Instance.HelpUseHerbicideAsync(_friendItemData.userId, ground.dirtData.key);
                EffectService.Instance.PlayVFX(VFXType.Herbicide, ground.transform.position, 1f);
                PlayExperiencesEf(ground.transform.position, ExperienceConstants.ExperienceFromActivity);
                AudioManager.Instance.PlaySFX(AudioName.Spray);
                TileBubbleController.Instance.HideBubble(ground.dirtData.key);
            }
            catch (Exception e)
            {
                DLogger.LogError("OnHelpHerbicidePlantAsync error: " + e.Message, "Ground");
            }
        }

        #endregion

        #endregion

        #region EF

        private void PlayHarvestEf(Vector3 positionSpawn, string itemRefId, int quantity)
        {
            var harvestObject = EffectService.Instance.PlayVFX(VFXType.Harvest, positionSpawn, 1f);
            var harvestEf     = harvestObject.GetComponent<HarvestEf>();

            var model = ResourceService.Instance.ModelGameObjectConfig.GetPlant(itemRefId);
            harvestEf.Init(model.GameHarvestIcon, quantity, 4);
        }

        private void PlayExperiencesEf(Vector3 positionSpawn, int experienceEarn)
        {
            var rectPosition = Camera.main!.WorldToScreenPoint(positionSpawn);


            var vfxObj     = EffectService.Instance.PlayVFX(VFXType.Experience, rectPosition);
            var vfxControl = vfxObj.GetComponent<ExperienceEf>();

            var targetFly    = _friendItemData == null ? _gameView.ExperienceBar : _visitView.ExperienceBar;
            var parentCanvas = _friendItemData == null ? _gameView.transform : _visitView.transform;

            vfxObj.transform.SetParent(parentCanvas);
            vfxObj.transform.localScale = Vector3.one;
            vfxControl.InitEf(targetFly, experienceEarn, () => { NakamaUserService.Instance.LoadPlayerStatsAsync(); });
        }

        #endregion
    }
}