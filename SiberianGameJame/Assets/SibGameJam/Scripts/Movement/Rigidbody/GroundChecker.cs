using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private LayerMask ground;

    public event Action HitGround;
    
    private void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, distance, ground))
            HitGround?.Invoke();
    }
}