using CiFarm.Scripts.SceneController.Game.PlantCore;
using CiFarm.Scripts.Services;
using CiFarm.Scripts.Services.NakamaServices;
using CiFarm.Scripts.UI.Popups;
using CiFarm.Scripts.UI.View;
using CiFarm.Scripts.UI.View.GameViewComponent;
using Imba.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CiFarm.Scripts.SceneController.Game
{
    public class EditModeController : MonoBehaviour
    {
        [SerializeField] private TileMapController tileMapController;
        [SerializeField] private CameraController  cameraController;

        private EditView _editView;

        private GameObject    _controllingItem;
        private InvenItemData _invenItemData;

        private Vector2Int _currentPosition;

        private bool _isInit;
        private bool _isPause;

        private void Awake()
        {
            _editView = UIManager.Instance.ViewManager.GetViewByName<EditView>(UIViewName.EditView);
        }

        public void Update()
        {
            if (!_isInit || _isPause)
            {
                return;
            }

            if (_editView.ToolManager.CurrentTool.toolType == ToolType.PlacingItem)
            {
                cameraController.LockCamera();
                _controllingItem.SetActive(true);
                _currentPosition = tileMapController.SetFakeGround(Input.mousePosition, _controllingItem);

                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    ShowConfirmPopup();
                }

                return;
            }

            if (_editView.ToolManager.CurrentTool.toolType == ToolType.Moving)
            {
                _controllingItem.SetActive(false);
                cameraController.UnLockCamera();
            }
        }

        public void SetUpEditMode(InvenItemData data)
        {
            _invenItemData = data;
            var prefabDirtData =
                ResourceService.Instance.ModelGameObjectConfig.GetTileObjectModel(_invenItemData.referenceKey);
            _controllingItem = SimplePool.Spawn(prefabDirtData, Vector3.zero, prefabDirtData.transform.rotation);

            _controllingItem.SetActive(false);
            tileMapController.DisplayAvailableToPlacingItem();
            _isInit  = true;
            _isPause = false;
        }

        public void ExitEditMode()
        {
            tileMapController.ClearAvailableToPlacingItem();
            _controllingItem.SetActive(false);
            _isInit  = false;
            _isPause = false;
            GameController.Instance.ExitEditMode();
        }

        public void ShowConfirmPopup()
        {
            _isPause = true;
            UIManager.Instance.PopupManager.ShowMessageDialog("Confirm", "Are you sure to place crop to this position",
                UIMessageBox.MessageBoxType.Yes_No, (st) =>
                {
                    if (st == UIMessageBox.MessageBoxAction.Accept)
                    {
                        OnConfirmPlaceDirt();
                    }

                    _isPause = false;
                    return true;
                });
        }

        public async void OnConfirmPlaceDirt()
        {
            UIManager.Instance.ShowLoading();
            _controllingItem.SetActive(false);
            await NakamaEditFarmService.Instance.PlaceTileRpcAsync(_invenItemData.key, new Position
            {
                x = _currentPosition.x,
                y = _currentPosition.y
            });
            ExitEditMode();
            UIManager.Instance.HideLoading();
        }
    }
}