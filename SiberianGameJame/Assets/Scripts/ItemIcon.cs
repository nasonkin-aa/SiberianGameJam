using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{

    [SerializeField] string[] ItemFormFactor;
    Vector2Int originPos;

    private void Awake()
    {
        if (ItemFormFactor.Length == 0)
        {
            ItemFormFactor = new string[] { "1" };
        }

        transform.GetComponent<Image>().raycastTarget = false;
    }

    public void SetOriginPosition(Vector2Int originPos)
    {
        this.originPos = originPos;
    }

    public Vector2Int GetOriginPosition()
    {
        return originPos;
    }

    public Vector2Int GetItemSizes()
    {
        return new Vector2Int(ItemFormFactor[0].Length, ItemFormFactor.Length);
    }

    public string[] GetItemFormFactor()
    {
        return ItemFormFactor;
    }

    public void MoveItemOnGrid(Vector2 pos)
    {
        //transform.position = new Vector2(x, y);
        transform.localPosition = pos;
    }

    public void MoveItem(Vector2 pos)
    {
        //transform.position = new Vector2(x, y);
        transform.position = pos;
    }

    public void SetParent(Transform transform)
    {
        this.transform.SetParent(transform);
    }

}
