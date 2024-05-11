using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "TileMap/TileData")]
public class TileData : ScriptableObject
{
    public TileBase[] tiles;

    public GameObject CanDestroy;
}
