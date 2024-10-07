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
        public string validTileName = "datBorder";

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

        public void SetAnyWithWithTilePos(Vector2Int position2D, GameObject objectPlaced)
        {
            var position           = new Vector3Int(position2D.x, position2D.y, 0);
            var tileCenterPosition = gameTileMap.CellToWorld(position);

            TileBase tile = gameTileMap.GetTile(position);

            if (tile != null && tile.name == interactableTileName)
            {
                // Adjust the position to the bottom-left corner of the tile
                tileCenterPosition -= new Vector3(gameTileMap.cellSize.x / 2.0f, gameTileMap.cellSize.y / 2.0f, 0);

                objectPlaced.transform.position = tileCenterPosition;
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

        public Vector2Int SetFakeGround(Vector3 position, GameObject objectPlaced, Vector2Int itemSize)
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(position);
            worldPosition.z = 0;

            var cellPosition       = interactableMap.WorldToCell(worldPosition);
            var tileCenterPosition = interactableMap.CellToWorld(cellPosition);

            var isValid = true;

            if (PlacedPositionHashSet.Contains((Vector2Int)cellPosition))
            {
                return (Vector2Int)cellPosition;
            }

            for (int x = 0; x < itemSize.x; x++)
            {
                for (int y = 0; y < itemSize.y; y++)
                {
                    var checkPosition = new Vector3Int(cellPosition.x + x, cellPosition.y + y, cellPosition.z);
                    var tile          = interactableMap.GetTile(checkPosition);

                    if (tile == null || tile.name != validTileName)
                    {
                        isValid = false;
                        break;
                    }
                }

                if (!isValid)
                {
                    break;
                }
            }

            if (isValid)
            {
                // tileCenterPosition.z            =  0;
                // tileCenterPosition.y            += interactableMap.cellSize.y / 2.0f;
                // Adjust the position to the bottom-left corner of the tile
                tileCenterPosition -= new Vector3(gameTileMap.cellSize.x / 2.0f, gameTileMap.cellSize.y / 2.0f, 0);

                objectPlaced.transform.position =  tileCenterPosition;
            }

            return (Vector2Int)cellPosition;
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