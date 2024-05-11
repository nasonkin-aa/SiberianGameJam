using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
 
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Music();
    }

    [SerializeField] private EventReference MineDig;
    [SerializeField] private EventReference MusicGame;
    [SerializeField] private EventReference Stone;
    [SerializeField] private EventReference MonsterWalk;
    [SerializeField] private EventReference PlasmaGun;
    [SerializeField] private EventReference RobotBipBop;
    [SerializeField] private EventReference RobotDamage;

    public void PlaySoundDig()
    {
        RuntimeManager.PlayOneShot(MineDig);
    }
    
    public void PlaySoundStone()
    {
        RuntimeManager.PlayOneShot(Stone);
    }
    
    public void PlayMonsterWalk()
    {
        RuntimeManager.PlayOneShot(MonsterWalk);
    }
    
    public void PlayPlasmaGun()
    {
        RuntimeManager.PlayOneShot(PlasmaGun);
    }
    
    public void PlayRobotBipBop()
    {
        RuntimeManager.PlayOneShot(RobotBipBop);
    }
    
    public void PlayRobotDamage()
    {
        RuntimeManager.PlayOneShot(RobotDamage);
    }

    private void Music()
    {
        RuntimeManager.PlayOneShot(MusicGame);
    }
}
