public interface IAttackable
{
    void Attack(IDamageable target);
    bool CanAttack { get; }

    bool TryAttack(IDamageable target)
    {
        if (CanAttack)
            Attack(target);

        return CanAttack;
    }
}