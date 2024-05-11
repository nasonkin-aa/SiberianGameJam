using UnityEngine;

[RequireComponent(typeof(Entity))]
public class PlayerController : MonoBehaviour
{
    private Entity _entity;

    private void Awake()
    {
        _entity = GetComponent<Entity>();
    }

    private void Update()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
        var yInput = Input.GetAxisRaw("Vertical");

        var delta = new Vector2(xInput, yInput);
        _entity.Moveable.MoveBy(delta);

        if (Input.GetKeyDown(KeyCode.Space))
            _entity.Jumpable.TryJump();
    }
}