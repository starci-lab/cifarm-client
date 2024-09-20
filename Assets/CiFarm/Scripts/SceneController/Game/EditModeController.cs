using CiFarm.Scripts.Services;
using CiFarm.Scripts.UI.Popups;
using CiFarm.Scripts.UI.View;
using CiFarm.Scripts.UI.View.GameViewComponent;
using Imba.UI;
using UnityEngine;

namespace CiFarm.Scripts.SceneController.Game
{
    public class EditModeController : MonoBehaviour
    {
        [SerializeField] private TileMapController tileMapController;
        [SerializeField] private CameraController  cameraController;

        private EditView _editView;

        private GameObject    _controllingItem;
        private InvenItemData _invenItemData;

        private bool isInit;

        private void Awake()
        {
            _editView = UIManager.Instance.ViewManager.GetViewByName<EditView>(UIViewName.EditView);
        }

        public void Update()
        {
            if (!isInit)
            {
                return;
            }

            if (_editView.ToolManager.CurrentTool.toolType == ToolType.PlacingItem)
            {
                cameraController.LockCamera();
                _controllingItem.SetActive(true);
                tileMapController.SetFakeGround(Input.mousePosition, _controllingItem);

                if (Input.GetMouseButtonDown(0))
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
            isInit = true;
        }

        public void ExitEditMode()
        {
            tileMapController.ClearAvailableToPlacingItem();
            _controllingItem.SetActive(false);
            isInit = false;
        }

        public void ShowConfirmPopup()
        {
            UIManager.Instance.PopupManager.ShowMessageDialog("Confirm", "Are you sure to place crop to this position",
                UIMessageBox.MessageBoxType.Yes_No, (st) =>
                {
                    if (st == UIMessageBox.MessageBoxAction.Accept)
                    {
                        OnConfirmPlaceDirt();
                    }
                    else
                    {
                        
                    }
                    return true;
                });
        }

        public void OnConfirmPlaceDirt()
        {
            _controllingItem.SetActive(false);
        }
    }
}