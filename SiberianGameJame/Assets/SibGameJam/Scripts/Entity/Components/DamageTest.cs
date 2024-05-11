using UnityEngine;

public class DamageTest : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 2;
    
    public bool CanTakeDamage => true;
    public bool CanHeal => true;

    public void TakeDamage(float amount, ITargetable source = null)
    {
        health -= amount;
        
        if (health <= 0)
            Destroy(gameObject);
    }
    public void Heal(float amount, ITargetable source = null) => health += amount;
}