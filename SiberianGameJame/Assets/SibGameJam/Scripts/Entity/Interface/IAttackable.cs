public interface IAttackable
{
    float Damage { get; }

    void Attack();
    bool CanAttack { get; }

    bool TryAttack()
    {
        if (CanAttack)
            Attack();

        return CanAttack;
    }
}