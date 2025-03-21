using System;
using FMODUnity;
using UnityEngine;

[Serializable]
public class AmbientOneShot
{
    public EventReference eventClip;
    public bool IsEnabled { get; set; } = false;
    public AudioClip audiolopl;
}
