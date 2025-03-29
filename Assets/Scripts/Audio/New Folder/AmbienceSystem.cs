using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using FMOD.Studio;
using FMODUnity;
using PeteUnityUtils.MinMaxSlider;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;


public class AmbienceSystem : MonoBehaviour
{
    public List<EventReference> AmbienceClips;

    private List<EventInstance> AmbienceEventInstances = new List<EventInstance>();
    
    //List of Serializeable Components 
        // Event, lower range cooldown time, upper range cooldown time, accessor is enabled flag
    public List<AmbientOneShot> ambientOneShots;


    private void Awake()
    {
        InitSystem();
        StartAmbienceSystem();
    }
    
    private void InitSystem()
    {
        foreach (var ambiClip in AmbienceClips)
        {
            var instance = RuntimeManager.CreateInstance(ambiClip);
            AmbienceEventInstances.Add(instance);
        }
        
       
    }
    
    [ProButton]
    public void StartAmbienceSystem()
    {
        foreach (var instance in AmbienceEventInstances)
        {
            instance.start();
        }
        EnableAmbienceOneShots();
    }

    private void EnableAmbienceOneShots()
    {
        foreach (var ambientOneShot in ambientOneShots)
        {
            //create go 
            var go = new GameObject();
            var ambiMono = go.AddComponent<AmbiOneShotMono>();
            ambiMono.SetAmbientOneShot(ambientOneShot);
            go.transform.SetParent(this.transform);


        }
    }

    [ProButton]
    public void StopAmbienceSystem()
    {
        foreach (var instance in AmbienceEventInstances)
        {
            instance.stop(STOP_MODE.ALLOWFADEOUT);
        }

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
    }

}
