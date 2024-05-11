using System;
using UnityEngine;

public class InventoryHUD : MonoBehaviour
{
    
    public event Action Opened;
    public event Action Closeed;

    public void Start()
    {
        gameObject.SetActive(false);
    }
    public void OpenInventory()
    {
        gameObject.SetActive(true);
        Opened?.Invoke();
    }

    public void CloseInventory()
    {
        gameObject.SetActive(false);
        Closeed?.Invoke();
    }

    public bool GetStatus()
    {
        return gameObject.activeSelf;
    }

}
