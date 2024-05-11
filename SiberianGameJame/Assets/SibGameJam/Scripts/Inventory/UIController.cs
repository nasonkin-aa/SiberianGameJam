using UnityEngine;

public class UIController : MonoBehaviour
{
    
    [SerializeField] InventoryGrid activeGrid;
    [SerializeField] CharacterSlot activeSlot;

    ItemIcon liftedItem;

    private void Update()
    {

        if (liftedItem != null)
        {
            liftedItem.MoveItem(Input.mousePosition);

            if (Input.GetMouseButtonDown(1))
            {
                liftedItem.RotateDirection();
            }
        }

        if (activeGrid == null && activeSlot == null) { return; }

        if (activeGrid != null)
        {
            Vector2Int gridMousePosition = activeGrid.GetGridPosition(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (liftedItem == null)
                {
                    activeGrid.LiftItem(gridMousePosition);
                }
                else
                {
                    if (!activeGrid.IsSlotEmpty(gridMousePosition))
                    {
                        activeGrid.SwapLiftedPlaced(gridMousePosition);
                        //Debug.Log(gridMousePosition);
                    }
                    else
                    {
                        activeGrid.PlaceItem(gridMousePosition, liftedItem);
                        //Debug.Log(gridMousePosition);
                    }
                    //Debug.Log(activeGrid.GetGridPosition(Input.mousePosition));
                }
            }
        }

        if (activeSlot != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (liftedItem == null)
                {
                    activeSlot.LiftItem();
                }
                else
                {
                    if (!activeSlot.IsSlotEmpty())
                    {
                        activeSlot.SwapLiftedPlaced();
                    }
                    else
                    {
                        activeSlot.PlaceItem(liftedItem);
                    }
                }
            }
        }

    }

    public void SetActiveGrid()
    {
        activeGrid = null;
    }

    public void SetActiveGrid(InventoryGrid grid)
    {
        activeGrid = grid;
    }

    public void SetActiveSlot()
    {
        activeSlot = null;
    }

    public void SetActiveSlot(CharacterSlot slot)
    {
        activeSlot = slot;
    }

    public void UnsetLiftedItem()
    {
        liftedItem = null;
    }

    public void SetLiftedItem(ItemIcon item)
    {
        liftedItem = item;
    }

    public ItemIcon GetLiftedItem()
    {
        return liftedItem;
    }
}
