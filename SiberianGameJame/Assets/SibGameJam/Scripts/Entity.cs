using UnityEngine;

[RequireComponent(typeof(IMoveable))]
public class Entity : MonoBehaviour
{
    public IMoveable Moveable { get; private set; }
    public IJumpable Jumpable { get; private set; }

    private void Awake()
    {
        Moveable = GetComponent<IMoveable>();
        Jumpable = GetComponent<IJumpable>();
    }

    private void Update()
    {
        Moveable.Move();
    }
}