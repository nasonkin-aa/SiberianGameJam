using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileMapManager : MonoBehaviour
{
        [SerializeField]
        private Tilemap map;
        
        [SerializeField]
        private List<TileData> tileData;
    
       // private Dictionary<TileBase, TileData> dataFromTiles;
    
        public static List<Vector3Int>  nonNullTiles = new List<Vector3Int>();
        
        public static int CountTile = 0;
        
        private void Awake()
        {
            GetCountTileInTilemap();
        }
        
        public void GetCountTileInTilemap()
        {
            BoundsInt bounds = map.cellBounds;
            TileBase[] allTiles = map.GetTilesBlock(bounds);
            var i = 0;
            for (int x = 0; x < bounds.size.x; x++)
                for (int y = 0; y < bounds.size.y; y++)
                {
                    map.SetTileFlags(new Vector3Int(x,y) + bounds.min,TileFlags.None);
                    TileBase tile = allTiles[x + y * bounds.size.x];
                    
                    if (tile != null)
                        nonNullTiles.Add(new Vector3Int(x,y) + bounds.min);
                }   
        }
}
