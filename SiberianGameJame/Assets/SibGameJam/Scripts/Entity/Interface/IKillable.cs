using System;

public interface IKillable
{
    Action DeadEvent { get; }
    void OnDeath(IDamageable damageable);
}