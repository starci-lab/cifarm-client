using UnityEngine;

namespace CiFarm.Scripts.SceneController.Game.PlantCore
{
    public class BaseGround : MonoBehaviour
    {
        [SerializeField] private Transform positionPlant;
        [SerializeField] private BasePlant plant;

        public void SetPlant(BasePlant plantToSet)
        {
            plant = plantToSet;
            plant.transform.SetParent(transform);
            plant.transform.position = positionPlant.position;
        }
    }
}