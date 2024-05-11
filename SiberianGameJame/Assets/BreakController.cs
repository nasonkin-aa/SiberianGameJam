using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BreakController : MonoBehaviour
{
    public List<ModuleBreak> breakers;
    public int delayBetweenBreaks = 2;
    public event Action<ModuleBreak> moduleBreak;
    private bool _isBreakPossible = true;
    void Start()
    {
        breakers = new List<ModuleBreak>(GetComponentsInChildren<ModuleBreak>());
        breakers.ForEach(module => module.moduleBreak += OnModuleBreak);
    }

    void OnModuleBreak()
    {
        List<Arm> arms = GetComponentsInChildren<Arm>().ToList();
        
        bool isHold = false;
        if (arms.Count > 0)
            foreach (var arm in arms)
                isHold |= arm.ArmState == ArmState.Holding;
        if (isHold)
            return;

        if (!_isBreakPossible || breakers.Count < 1)
            return;

        var number = UnityEngine.Random.Range(0, breakers.Count);
        moduleBreak?.Invoke(breakers[number]);
        breakers[number].ModuleSetActive(false);
        breakers.Remove(breakers[number]);
        StartCoroutine(TimeOutForBreak());
    }

    void OnDestroy()
    {
        breakers.ForEach(module => module.moduleBreak -= OnModuleBreak);
    }
    IEnumerator TimeOutForBreak()
    {
        _isBreakPossible = false;
        yield return new WaitForSeconds(delayBetweenBreaks);
        _isBreakPossible = true;
    }
}
