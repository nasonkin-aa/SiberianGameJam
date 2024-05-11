using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
