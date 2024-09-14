using System;
using System.Collections.Generic;
using CiFarm.Scripts.SceneController.Game.PlantCore;
using CiFarm.Scripts.Services;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.UI.Popups;
using CiFarm.Scripts.UI.View;
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

        private HashSet<BaseGround> _baseGrounds;
        private GameView         _gameView;

        #region GETTET SETTER

        public TileMapController TileMapController => tileMapController;
        public CameraController  CameraController  => cameraController;

        #endregion

        private void Start()
        {
            _baseGrounds = new HashSet<BaseGround>();
            UIManager.Instance.ViewManager.ShowView(UIViewName.GameView);
            _gameView = UIManager.Instance.ViewManager.GetViewByName<GameView>(UIViewName.GameView);
            _gameView.Show();
            LoadUserTileMap();
            NakamaSocketService.Instance.OnFetchPlacedDataFromServer = OnFetchPlacedDataFromServer;
            // SHOW TO UI
            // LoadNormalDirt();
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
            foreach (var dirt in
                     _baseGrounds) 
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

        #endregion
    }
}