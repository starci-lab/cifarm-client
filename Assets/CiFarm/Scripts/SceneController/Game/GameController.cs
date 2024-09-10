using System;
using System.Collections.Generic;
using CiFarm.Scripts.SceneController.Game.PlantCore;
using Imba.UI;
using Imba.Utils;
using SupernovaDriver.Scripts.UI.View;
using UnityEngine;

namespace CiFarm.Scripts.SceneController.Game
{
    public class GameController : ManualSingletonMono<GameController>
    {
        [SerializeField] private TileMapController tileMapController;

        private GameView _gameView;

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
        public List<DirtPlantTest> listDirtNft;

        private void Start()
        {
            _gameView = UIManager.Instance.ViewManager.GetViewByName<GameView>(UIViewName.GameView);
            LoadNormalDirt();
            LoadNftDirt();
        }

        private void Update()
        {
            // if (Input.GetMouseButtonDown(0))
            // {
            //     tileMapController.SetGround(Input.mousePosition, dirtTile);
            // }
        }

        public void LoadNormalDirt()
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

        public void LoadNftDirt()
        {
            foreach (var dt in listDirtNft)
            {
                var dirtObj  = Instantiate(dirtTileNft);
                var plantObj = Instantiate(dt.plant);

                var dirtScript = dirtObj.GetComponent<BaseGround>();
                var plant      = plantObj.GetComponent<BasePlant>();

                plant.SetPlantState(dt.plantState);
                dirtScript.SetPlant(plant);

                tileMapController.SetGroundWithTilePos(dt.position, dirtObj);
            }
        }
    }
}