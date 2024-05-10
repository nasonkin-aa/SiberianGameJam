using UnityEngine;

[RequireComponent(typeof(IMoveable))]
public class PlayerController : MonoBehaviour
{
    private IMoveable _moveable;

    private void Awake()
    {
        _moveable = GetComponent<IMoveable>();
    }

    private void Update()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
        var yInput = Input.GetAxisRaw("Vertical");

        var delta = new Vector2(xInput, yInput);
        _moveable.MoveBy(delta);
    }
}