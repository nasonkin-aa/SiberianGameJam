using System;

public interface IDamageable
{
    bool CanTakeDamage { get; }
    bool CanHeal { get; }

    void TakeDamage(float amount, ITargetable source = null);
    void Heal(float amount, ITargetable source = null);

    bool TryTakeDamage(float amount, ITargetable source = null)
    {
        if (CanTakeDamage) TakeDamage(amount, source);
        return CanTakeDamage;
    }

    bool TryHeal(float amount, ITargetable source = null)
    {
        if (CanHeal) Heal(amount, source);
        return CanHeal;
    }
}