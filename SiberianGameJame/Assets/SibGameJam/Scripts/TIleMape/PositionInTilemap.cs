using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PositionInTilemap : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;
    [SerializeField]
    private TileMapManager mapManager;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SetPositionInTilemap();
        }
    }

    public void SetPositionInTilemap()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);
        if (gridPosition != null)
        {
            
            /*map.SetColor(gridPosition, Color.red);
            map.SetTile(gridPosition, null);
            Debug.Log(gridPosition);*/
            //AudioManager.instance.PlaySoundDig();
        }
    }
}
