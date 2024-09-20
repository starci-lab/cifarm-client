using System.Collections.Generic;
using CiFarm.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CiFarm.Scripts.SceneController.Game
{
    public class TileMapController : MonoBehaviour
    {
        [Header("Game tile")]
        public Tilemap gameTileMap;

        public Tile   hiddenInteractableTile;
        public string interactableTileName        = "interactions";
        public string interactableTileVisibleName = "interactions_visible";

        [Header("Tile Interactions")]
        public Tilemap interactableMap;

        public Tile   validTile;
        public string validTileName = "";

        public HashSet<Vector2Int> PlacedPositionHashSet;

        private void Awake()
        {
            PlacedPositionHashSet = new HashSet<Vector2Int>();
            foreach (var position in gameTileMap.cellBounds.allPositionsWithin)
            {
                TileBase tile = gameTileMap.GetTile(position);
                if (tile != null && tile.name == interactableTileVisibleName)
                {
                    gameTileMap.SetTile(position, hiddenInteractableTile);
                }
            }
        }

        public void SetGroundWithTilePos(Vector2Int position2D, GameObject objectPlaced)
        {
            var position           = new Vector3Int(position2D.x, position2D.y, 0);
            var tileCenterPosition = gameTileMap.CellToWorld(position);

            TileBase tile = gameTileMap.GetTile(position);

            if (tile != null && tile.name == interactableTileName)
            {
                tileCenterPosition.z            =  0;
                tileCenterPosition.y            += gameTileMap.cellSize.y / 2.0f;
                objectPlaced.transform.position =  tileCenterPosition;
                PlacedPositionHashSet.Add(position2D);
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

        public void ResetPosition()
        {
            PlacedPositionHashSet.Clear();
        }

        public void SetGround(Vector3 position, GameObject objectPlaced)
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(position);
            worldPosition.z = 0;

            var cellPosition       = gameTileMap.WorldToCell(worldPosition);
            var tileCenterPosition = gameTileMap.CellToWorld(cellPosition);
            DLogger.Log("Clicked Tile: " + cellPosition, "TileManager", LogColors.Lime);

            TileBase tile = gameTileMap.GetTile(cellPosition);

            if (tile != null && tile.name == interactableTileName)
            {
                tileCenterPosition.z =  0;
                tileCenterPosition.y += gameTileMap.cellSize.y / 2.0f;
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

        public Vector2Int SetFakeGround(Vector3 position, GameObject objectPlaced)
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(position);
            worldPosition.z = 0;

            var cellPosition       = interactableMap.WorldToCell(worldPosition);
            var tileCenterPosition = interactableMap.CellToWorld(cellPosition);

            TileBase tile = interactableMap.GetTile(cellPosition);

            if (tile != null && tile.name == validTileName)
            {
                tileCenterPosition.z            =  0;
                tileCenterPosition.y            += interactableMap.cellSize.y / 2.0f;
                objectPlaced.transform.position =  tileCenterPosition;
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

            return (Vector2Int)cellPosition;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                DLogger.Log("Gotcha");
                DisplayAvailableToPlacingItem();
            }
        }

        public void DisplayAvailableToPlacingItem()
        {
            foreach (var position in gameTileMap.cellBounds.allPositionsWithin)
            {
                TileBase tile = gameTileMap.GetTile(position);
                if (tile != null && tile.name == interactableTileName)
                {
                    interactableMap.SetTile(position, validTile);
                }
            }
        }

        public void ClearAvailableToPlacingItem()
        {
            foreach (var position in interactableMap.cellBounds.allPositionsWithin)
            {
                TileBase tile = interactableMap.GetTile(position);
                if (tile != null)
                {
                    interactableMap.SetTile(position, null);
                }
            }
        }
    }
}