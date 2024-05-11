using UnityEngine;

public class Entity : MonoBehaviour, ITargetable
{
    public IMoveable Moveable { get; private set; }
    public IJumpable Jumpable { get; private set; }
    public IAttackable Attackable { get; private set; }

    private void Awake()
    {
        Moveable = GetComponent<IMoveable>();
        Jumpable = GetComponent<IJumpable>();
        Attackable = GetComponent<IAttackable>();
    }

    private void Update()
    {
        Moveable?.Handle();
        Jumpable?.Handle();
    }

    public Vector3 Position => transform.position;
}