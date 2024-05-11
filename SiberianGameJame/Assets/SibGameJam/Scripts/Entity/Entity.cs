using UnityEngine;

public class Entity : MonoBehaviour, ITargetable
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
        Moveable?.Move();
        Jumpable?.HandleLogic();
    }

    public Vector3 Position => transform.position;
}