using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpace : MonoBehaviour
{
    public virtual void PlaceItem() { }
    public virtual void LiftItem() { }
    public virtual void SwapLiftedPlaced() { }
}
