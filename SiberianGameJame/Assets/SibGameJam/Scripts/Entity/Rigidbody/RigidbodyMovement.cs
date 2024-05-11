using Tools;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyMovement : MonoBehaviour, IJumpable, IMoveable
{
    [Header("Moveable")]
    [SerializeField] private float speed;
    [SerializeField] private float range = 1;

    [Header("Jumpable")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private GroundChecker groundChecker;
        
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    #region IMoveable

    public Vector3 Position => transform.position;
    public Vector2 Direction { get; private set; }
    public Vector2 Velocity => _rigidbody2D.velocity;

    public void MoveBy(Vector3 delta) => Direction = delta.normalized;
    void IMoveable.Handle() => _rigidbody2D.velocity = Velocity.With(Direction.x * speed);

    public void LookAt(Vector3 position)
    {
        var delta = position - Position;
        transform.rotation = delta.x > 0 ? FacingRight : FacingLeft;
    }

    private static Quaternion FacingRight => Quaternion.Euler(0, 0, 0);
    private static Quaternion FacingLeft => Quaternion.Euler(0, 180, 0);
    
    public bool Reached(Vector3 position) => position.InRange(Position, range);

    #endregion

    #region IJumpable

    public bool CanJump { get; private set; }
    public void Jump()
    {
        _rigidbody2D.velocity = Velocity.With(null,
            Mathf.Sqrt(jumpHeight * _rigidbody2D.gravityScale * Physics2D.gravity.y * -1));

        CanJump = false;
    }

    void IJumpable.Handle()
    {
        if (groundChecker.OnGround)
            CanJump = true;
    }

    #endregion
}