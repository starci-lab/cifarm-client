using Imba.UI;
using Imba.Utils;
using SupernovaDriver.Scripts.UI.View;

namespace CiFarm.Scripts.SceneController.Game
{
    public class GameController : ManualSingletonMono<GameController>
    {


        private GameView _gameView;

        private void Start()
        {
            _gameView = UIManager.Instance.ViewManager.GetViewByName<GameView>(UIViewName.GameView);
        }

  
    }
}