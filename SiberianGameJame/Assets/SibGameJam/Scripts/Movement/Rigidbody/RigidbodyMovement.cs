using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyMovement : MonoBehaviour, IMoveable
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float range = 1;
        
    private Rigidbody2D _rigidbody2D;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    #region IMoveable

    public Vector3 Position => transform.position;

    public void MoveTo(Vector3 position) => _direction = (position - Position).normalized;

    public void MoveBy(Vector3 delta) => _direction = delta.normalized;

    public void Move()
    {
        // var diff =  maxSpeed - _rigidbody2D.velocity.magnitude;
        // _rigidbody2D.AddForce(Vector2.right * _direction * (speed * diff));

        var velocity = new Vector2(_direction.x * speed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = velocity;
    }

    public bool Reached(Vector3 position) => (position - Position).sqrMagnitude <= range * range;

    #endregion
}