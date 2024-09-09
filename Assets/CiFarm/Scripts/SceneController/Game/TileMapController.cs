using SupernovaDriver.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CiFarm.Scripts.SceneController.Game
{
    public class TileMapController : MonoBehaviour
    {
        public Tilemap interactableMap;

        public Tile   hiddenInteractableTile; // Tile to replace "interactions_visible" tiles with
        public string interactableTileName = "interactions"; // Name of the tile to check for

        void Start()
        {
            foreach (var position in interactableMap.cellBounds.allPositionsWithin)
            {
                TileBase tile = interactableMap.GetTile(position);
                if (tile != null && tile.name == "interactions_visible")
                {
                    interactableMap.SetTile(position, hiddenInteractableTile);
                }
            }
        }

        public void SetGround(Vector3 position, GameObject objectPlaced)
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(position);
            worldPosition.z = 0;

            var cellPosition       = interactableMap.WorldToCell(worldPosition);
            var tileCenterPosition = interactableMap.CellToWorld(cellPosition);


            TileBase tile = interactableMap.GetTile(cellPosition);

            if (tile != null && tile.name == interactableTileName)
            {
                tileCenterPosition.z =  0;
                tileCenterPosition.y += interactableMap.cellSize.y / 2.0f;
                var obj = Instantiate(objectPlaced);
                obj.transform.position = tileCenterPosition;
            }
            else
            {
                if (tile == null)
                {
                    DLogger.Log("No tile at this position (NULL)", "TileManager", LogColors.Lime);
                }
                else
                {
                    DLogger.Log($"Tile name: {tile.name}", "TileManager", LogColors.Lime);
                }
            }
        }
    }
}