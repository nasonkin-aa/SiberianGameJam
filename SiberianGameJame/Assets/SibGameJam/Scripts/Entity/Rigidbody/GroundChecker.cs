using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private LayerMask ground;

    public bool OnGround { get; private set; }
    
    public event Action HitGround;
    
    private void Update()
    {
        OnGround = Physics2D.Raycast(transform.position, Vector2.down, distance, ground);
        if (OnGround)
            HitGround?.Invoke();
    }
}