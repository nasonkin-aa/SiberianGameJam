using UnityEngine;

public class DamageTest : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    public bool CanTakeDamage => true;
    public bool CanHeal => true;

    public void TakeDamage(float amount, ITargetable source = null) => health -= amount;
    public void Heal(float amount, ITargetable source = null) => health += amount;
}