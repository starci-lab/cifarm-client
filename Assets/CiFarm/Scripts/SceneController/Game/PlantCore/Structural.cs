using Imba.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CiFarm.Scripts.SceneController.Game.PlantCore
{
    public class Structural : MonoBehaviour
    {
        public string structuralId = "";

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.StructuralDetailPopup);
            
        }
    }
}