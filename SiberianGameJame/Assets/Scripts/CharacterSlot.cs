using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] UIController controller;

    RectTransform rectTransform;

    ItemIcon storedItem;

    public void EquipItem(ItemPickupable item)
    {
        if (storedItem != null) return;

        ItemIcon equipedItem = GameObject.Instantiate(item.GetInventoryIcon(), this.transform);
        PlaceItem(equipedItem);
    }

    public void PlaceItem(ItemIcon item)
    {
        if (storedItem != null) return;

        storedItem = item;

        item.SetParent(transform);

        if (controller.GetLiftedItem() == item)
        {
            controller.UnsetLiftedItem();
        }

        DrawItem();
    }

    public void LiftItem()
    {
        if (storedItem == null) return;

        controller.SetLiftedItem(storedItem);
        storedItem = null;

        DrawItem();
    }

    public void SwapLiftedPlaced()
    {
        ItemIcon buffer = controller.GetLiftedItem();
        LiftItem();
        PlaceItem(buffer);
        DrawItem();
    }

    public bool IsSlotEmpty()
    {
        return storedItem == null;
    }

    void DrawItem()
    {
        if (storedItem == null) return;

        storedItem.MoveItem(transform.position);
    }

    public void ActivateSlot()
    {
        controller.SetActiveSlot(this);
    }

    public void DeactivateSlot()
    {
        controller.SetActiveSlot();
    }

}
