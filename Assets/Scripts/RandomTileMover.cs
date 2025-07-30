using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomTileMover : MonoBehaviour
{
    public Tilemap tilemap;              // Assign the tilemap in Inspector
    public TileBase targetTile;          // The tile to move
    public int numberOfTilesToMove = 5;  // Number of tiles to move
    public Vector2Int gridSize = new Vector2Int(10, 5); // Grid size range (x, y)

    void Start()
    {
        RandomizeTiles();
    }

    void RandomizeTiles()
    {
        // Step 1: Clear all existing tiles of target type
        for (int x = -gridSize.x; x <= gridSize.x; x++)
        {
            for (int y = -gridSize.y; y <= gridSize.y; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(pos) == targetTile)
                {
                    tilemap.SetTile(pos, null);
                }
            }
        }

        // Step 2: Set new random positions
        for (int i = 0; i < numberOfTilesToMove; i++)
        {
            Vector3Int randomPos = new Vector3Int(
                Random.Range(-gridSize.x, gridSize.x),
                Random.Range(-gridSize.y, gridSize.y),
                0
            );
            tilemap.SetTile(randomPos, targetTile);
        }
    }
}
