using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyJump : MonoBehaviour, IJumpable
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private GroundChecker groundChecker;

    private Rigidbody2D _rigidbody2D;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        groundChecker.HitGround += () => CanJump = true;
    }

    public float JumpHeight => jumpHeight;
    public bool CanJump { get; private set; }
    
    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        CanJump = false;
    }
}