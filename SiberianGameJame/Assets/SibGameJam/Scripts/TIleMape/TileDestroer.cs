using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDestroer : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    private int strength = 3;
    public List<TileData> TileDatas = new();
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        List<Vector3Int> positions = GetTilePositionsFromCollision(collision);
        foreach (var position in positions)
        {
            if (!map.HasTile(position)) continue;
            foreach (var tile in TileDatas)
            { 
                if (tile.CanDestroy.name == gameObject.name && tile.tiles.Contains(map.GetTile(position)))
                {
                    map.SetTile(position, null);
                    strength--;
                    if ( strength <= 0)
                    {
                        Destroy(gameObject);
                    }
                    AudioManager.instance.PlaySoundDig();
                    Debug.Log($"Tile removed at position {position}");
                }
            }
        }
    }

    private List<Vector3Int> GetTilePositionsFromCollision(Collision2D collision)
    {
        List<Vector3Int> tilePositions = new List<Vector3Int>();
        Vector2 hitPosition = Vector2.zero;

        foreach (ContactPoint2D hit in collision.contacts)
        {
            hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
            hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
            Vector3Int cellPosition = map.WorldToCell(hitPosition);
            tilePositions.Add(cellPosition);
        }
        return tilePositions;
    }
}
