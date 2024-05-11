using System;
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

    private void Awake()
    {
        map = GameObject.FindObjectOfType<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        List<Vector3Int> positions = GetTilePositionsFromCollision(collision);
        foreach (var position in positions)
        {
            if (!map.HasTile(position)) continue;
            foreach (var tile in TileDatas)
            { 
                Item itemComponent = gameObject.GetComponent<Item>();
                Debug.Log(tile.CanDestroy + " tiele");
                Debug.Log( itemComponent.GetType().Name +" obj");
                if (tile.CanDestroy.GetType().Name == itemComponent.GetType().Name && tile.tiles.Contains(map.GetTile(position)))
                {
                    map.SetTile(position, null);
                    gameObject.GetComponent<Item>().strength--;
                    if ( gameObject.GetComponent<Item>().strength <= 0)
                    {
                        Destroy(gameObject);
                    }
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
