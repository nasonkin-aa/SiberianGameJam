using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryGrid : MonoBehaviour
{

    [SerializeField] UIController UIController;

    [SerializeField] int gridWidth = 1;
    [SerializeField] int gridHeight = 1;

    private float tileWidth;
    private float tileHeight;

    private Vector2 positionOnRect;
    private Vector2Int tilePosition;

    RectTransform rectTransform;

    ItemIcon[,] items;
    public ItemIcon liftedItem;

    //[SerializeField] ItemIcon[] TestItems;

    private void Start()
    {
        tileWidth = GetComponent<Image>().sprite.rect.width;
        tileHeight = GetComponent<Image>().sprite.rect.height;

        float width = tileWidth * gridWidth;
        float height = tileHeight * gridHeight;

        rectTransform = GetComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(width, height);

        items = new ItemIcon[gridWidth, gridHeight];

        /*PlaceItem(new Vector2Int(1, 1), TestItems[0]);
        PlaceItem(new Vector2Int(4, 3), TestItems[1]);
        PlaceItem(new Vector2Int(2, 2), TestItems[2]);
        PlaceItem(new Vector2Int(0, 2), TestItems[3]);*/

    }

    public Vector2Int GetGridPosition(Vector2 mousePosition)
    {
        
        positionOnRect.x = mousePosition.x - rectTransform.position.x;
        positionOnRect.y = -(mousePosition.y - rectTransform.position.y);

        tilePosition.x = (int)math.floor(positionOnRect.x / tileWidth);
        tilePosition.y = (int)math.floor(positionOnRect.y / tileHeight);

        return tilePosition;
    }

    public void ActivateGrid()
    {
        UIController.SetActiveGrid(this);
    }

    public void DeactivateGrid()
    {
        UIController.SetActiveGrid();
    }


    public void LiftItem(Vector2Int tilePos)
    {
        if (items[tilePos.x, tilePos.y] == null) return;

        ItemIcon item = items[tilePos.x, tilePos.y];
        Vector2Int itemOriginPos = item.GetOriginPosition();

        UIController.SetLiftedItem(item);

        for (int i = 0; i < item.GetItemFormFactor().Length; i++)
        {
            for (int j = 0; j < item.GetItemFormFactor()[i].Length; j++)
            {
                if (item.GetItemFormFactor()[i][j] == '1')
                    items[itemOriginPos.x + j, itemOriginPos.y + i] = null;
            }
        }

        DrawItemIcons();
    }

    public void PlaceItem(Vector2Int tilePos, ItemIcon item)
    {
        if (!CanPlace( tilePos, item)) return;

        for (int i = 0; i < item.GetItemFormFactor().Length; i++)
        {
            for (int j = 0; j < item.GetItemFormFactor()[i].Length; j++)
            {
                if (item.GetItemFormFactor()[i][j] == '1')
                    items[tilePos.x + j, tilePos.y + i] = item;
            }
        }

        item.SetOriginPosition(tilePos);

        item.transform.SetParent(transform);

        if (UIController.GetLiftedItem() == item)
        {
            UIController.UnsetLiftedItem();
        }

        DrawItemIcons();
    }


    public void SwapLiftedPlaced(Vector2Int tilePos)
    {


        if (Intersection(tilePos, UIController.GetLiftedItem()).Count != 1) return;

        ItemIcon buffer = UIController.GetLiftedItem();
        LiftItem(tilePos);
        PlaceItem(tilePos, buffer);

        DrawItemIcons();
    }

    public bool IsSlotEmpty(Vector2Int pos)
    {
        return items[pos.x, pos.y] == null;
    }

    private void DrawItemIcons()
    {

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                if (items[i, j] != null)
                {
                    if (items[i, j].GetOriginPosition() == new Vector2Int(i, j))
                        items[i, j].MoveItemOnGrid(new Vector2(i * tileWidth, -j * tileHeight));
                }
            }
        }

    }

    private bool CanPlace(Vector2Int tilePos, ItemIcon item)
    {
        if ((tilePos.x + item.GetItemSizes().x > gridWidth) || (tilePos.y + item.GetItemSizes().y > gridHeight)) return false;

        for (int i = 0; i < item.GetItemFormFactor().Length; i++)
        {
            for (int j = 0; j < item.GetItemFormFactor()[i].Length; j++)
            {
                Vector2Int slotPos = new Vector2Int(j + tilePos.x, i + tilePos.y);
                if (!IsSlotEmpty(slotPos) && item.GetItemFormFactor()[i][j] != '0')
                {
                    return false;
                }
            }
        }

        return true;
    }

    private HashSet<ItemIcon> Intersection(Vector2Int tilePos, ItemIcon item)
    {
        HashSet<ItemIcon> intersectingItems = new HashSet<ItemIcon>();

        if ((tilePos.x + item.GetItemSizes().x > gridWidth) || (tilePos.y + item.GetItemSizes().y > gridHeight)) return intersectingItems;

        for (int i = 0; i < item.GetItemFormFactor().Length; i++)
        {
            for (int j = 0; j < item.GetItemFormFactor()[i].Length; j++)
            {
                Vector2Int slotPos = new Vector2Int(j + tilePos.x, i + tilePos.y);
                if (!IsSlotEmpty(slotPos) && item.GetItemFormFactor()[i][j] != '0')
                {
                    intersectingItems.Add(items[slotPos.x, slotPos.y]);
                }
            }
        }

        return intersectingItems;
    }
    
}
