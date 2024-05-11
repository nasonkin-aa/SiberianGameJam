public interface IDamageable
{
    bool CanTakeDamage { get; }
    bool CanHeal { get; }
    
    void TakeDamage(float amount);
    void Heal(float amount);

    bool TryTakeDamage(float amount)
    {
        if (CanTakeDamage) TakeDamage(amount);
        return CanTakeDamage;
    }

    bool TryHeal(float amount)
    {
        if (CanHeal) Heal(amount);
        return CanHeal;
    }
}