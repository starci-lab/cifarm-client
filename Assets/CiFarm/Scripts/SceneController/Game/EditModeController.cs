using UnityEngine;

namespace CiFarm.Scripts.SceneController.Game
{
    public class EditModeController : MonoBehaviour
    {
        [SerializeField] private TileMapController tileMapController;

        public void SetUpEditMode()
        {
            tileMapController.DisplayAvailableToPlacingItem();
        }

        public void ExitEditMode()
        {
            tileMapController.ClearAvailableToPlacingItem();
        }

    }
}