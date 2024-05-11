using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] UIController controller;

    public ItemIcon storedItem;

    public InventoryHUD hud;

    public void Awake()
    {
        hud.Opened += SetStartPos;
    }
    public void EquipItem(ItemIcon item)
    {
        if (storedItem != null) return;

        ItemIcon equipedItem = Instantiate(item, transform);
        PlaceItem(equipedItem);

        DrawItem();
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

    public void SetStartPos()
    {
        if (storedItem != null)
            storedItem.transform.localPosition = new Vector3(0, 0, 0);
    }

}
