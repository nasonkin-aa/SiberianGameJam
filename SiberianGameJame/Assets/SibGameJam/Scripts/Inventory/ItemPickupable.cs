using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class ItemPickupable : MonoBehaviour
{
    [SerializeField] ItemIcon inventoryIcon;
    [SerializeField] CharacterSlot characterSlotR;
    [SerializeField] CharacterSlot characterSlotL;

    public ItemIcon GetInventoryIcon()
    {
        return inventoryIcon;
    }

    public void GetPickedUp()
    {
        gameObject.SetActive(false);
        characterSlotR.EquipItem(this);
    }
}
