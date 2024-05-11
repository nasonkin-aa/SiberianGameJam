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
    
    [SerializeField] private EventReference MineDig;

    public void PlaySoundDig()
    {
        RuntimeManager.PlayOneShot(MineDig);
    }
}
