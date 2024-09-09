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

        [Header("Test")]
        public GameObject dirtTile;

        private void Start()
        {
            _gameView = UIManager.Instance.ViewManager.GetViewByName<GameView>(UIViewName.GameView);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                tileMapController.SetGround(Input.mousePosition, dirtTile);
            }
        }
    }
}