using System;
using CiFarm.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CiFarm.Scripts.SceneController.Game
{
    public class TileMapController : MonoBehaviour
    {
        public Tilemap interactableMap;

        public Tile   hiddenInteractableTile; // Tile to replace "interactions_visible" tiles with
        public string interactableTileName = "interactions"; // Name of the tile to check for

        private void Awake()
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

        public void SetGroundWithTilePos(Vector2Int position2D, GameObject objectPlaced)
        {
            var position           = new Vector3Int(position2D.x, position2D.y, 0);
            var tileCenterPosition = interactableMap.CellToWorld(position);

            TileBase tile = interactableMap.GetTile(position);

            if (tile != null && tile.name == interactableTileName)
            {
                tileCenterPosition.z =  0;
                tileCenterPosition.y += interactableMap.cellSize.y / 2.0f;
                objectPlaced.transform.position = tileCenterPosition;
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

        public void SetGround(Vector3 position, GameObject objectPlaced)
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(position);
            worldPosition.z = 0;

            var cellPosition       = interactableMap.WorldToCell(worldPosition);
            var tileCenterPosition = interactableMap.CellToWorld(cellPosition);
            DLogger.Log("Clicked Tile: " + cellPosition, "TileManager", LogColors.Lime);

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