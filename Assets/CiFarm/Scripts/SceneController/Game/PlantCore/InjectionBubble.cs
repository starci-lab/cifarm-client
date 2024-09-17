using UnityEngine;

namespace CiFarm.Scripts.SceneController.Game.PlantCore
{
    public class InjectionBubble : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer iconRender;
    }

    public enum InjectionType
    {
        Water,
        Worm,
        Grass
    }
}