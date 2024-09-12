using System;
using System.Collections.Generic;
using CiFarm.Scripts.SceneController.Game.PlantCore;
using CiFarm.Scripts.Services;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.UI.View;
using Imba.UI;
using Imba.Utils;
using SupernovaDriver.Scripts.UI.View;
using Unity.VisualScripting;
using UnityEngine;

namespace CiFarm.Scripts.SceneController.Game
{
    public class GameController : ManualSingletonMono<GameController>
    {
        [SerializeField] private TileMapController tileMapController;
        [SerializeField] private CameraController  cameraController;

        private GameView _gameView;

        #region GETTET SETTER

        public TileMapController TileMapController => tileMapController;
        public CameraController  CameraController  => cameraController;

        #endregion

        [Header("Test Zone Here, this play will make fake parameter")]
        public GameObject dirtTile;

        public GameObject dirtTileNft;

        [Serializable]
        public struct DirtPlantTest
        {
            public Vector2Int position;
            public GameObject plant;
            public int        plantState;
        }

        public List<DirtPlantTest> listDirtNormal;

        private void Start()
        {
            UIManager.Instance.ViewManager.ShowView(UIViewName.GameView);
            _gameView = UIManager.Instance.ViewManager.GetViewByName<GameView>(UIViewName.GameView);
            _gameView.Show();

            LoadUserTileMap();
            // SHOW TO UI
            // LoadNormalDirt();
        }

        private void Update()
        {
            // if (Input.GetMouseButtonDown(0))
            // {
            //     tileMapController.SetGround(Input.mousePosition, dirtTile);
            // }
        }

        private void LoadNormalDirt()
        {
            foreach (var dt in listDirtNormal)
            {
                var dirtObj  = Instantiate(dirtTile);
                var plantObj = Instantiate(dt.plant);

                var dirtScript = dirtObj.GetComponent<BaseGround>();
                var plant      = plantObj.GetComponent<BasePlant>();

                plant.SetPlantState(dt.plantState);
                dirtScript.SetPlant(plant);

                tileMapController.SetGroundWithTilePos(dt.position, dirtObj);
            }
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
            var prefabDirtData = ResourceService.Instance.ModelGameObjectConfig.GetTile(placedItem.referenceKey);
            var dirtObj        = Instantiate(prefabDirtData);

            tileMapController.SetGroundWithTilePos(
                new Vector2Int((int)placedItem.position.x, (int)placedItem.position.y)
                , dirtObj);

            if (placedItem.isPlanted)
            {
                var prefabPlantData = ResourceService.Instance.ModelGameObjectConfig.GetPlant(placedItem.referenceKey);
                var plantObj        = Instantiate(prefabPlantData);

                var plant      = plantObj.GetComponent<BasePlant>();
                var dirtScript = dirtObj.GetComponent<BaseGround>();
                plant.SetPlantState(placedItem.seedGrowthInfo.currentStage);
                dirtScript.SetPlant(plant);
            }
        }

        private void PlacedBuilding(PlacedItem placedItem)
        {
        }

        #endregion

        #endregion
    }
}