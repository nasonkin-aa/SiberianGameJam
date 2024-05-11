using UnityEngine;

public class AttackTest : MonoBehaviour, IAttackable
{
    [field: SerializeField]
    public float Damage { get; private set; }

    public bool CanAttack => true;

    public void Attack(IDamageable target) => target.TryTakeDamage(Damage);
}