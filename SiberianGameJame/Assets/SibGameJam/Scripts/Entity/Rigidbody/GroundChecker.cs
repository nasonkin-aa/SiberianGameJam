using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private LayerMask ground;

    public bool OnGround { get; private set; }

    private void Update()
    {
        OnGround = Physics2D.Raycast(transform.position, Vector2.down, distance, ground);
    }
}