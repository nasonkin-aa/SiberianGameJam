using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int strength = 3;
    
    protected Transform Hand;
    protected bool Attached = false;

    public virtual void AttachToHand(Transform hand)
    {
        Attached = true;
        Hand = hand;
    }

    public virtual void DetachFromHand()
    {
        Attached = false;
    }
}
