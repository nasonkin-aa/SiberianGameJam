using UnityEngine;

public class PlayerDamage : MonoBehaviour, IDamageable
{
    private BreakController _breakController;

    private void Awake()
    {
        _breakController = GetComponent<BreakController>();
    }

    public bool CanTakeDamage => true;
    public bool CanHeal => true;
    
    public void TakeDamage(float amount, ITargetable source = null) => _breakController.RemovedModule();

    public void Heal(float amount, ITargetable source = null) {}
}