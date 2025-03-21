using System.Collections;
using System.Collections.Generic;
using PeteUnityUtils.MinMaxSlider;
using UnityEngine;

public class AmbienceSystem : MonoBehaviour
{
    [MinMaxSlider(5, 20)]
    public Vector2 peteRange;
    //List of Event clips 
    //private list of instances, will be mapped to the event clips above


    //List of Serializeable Components 
    // Event, lower range cooldown time, upper range cooldown time, accessor is enabled flag
    public List<AmbientOneShot> ambientOneShots;

    //init system 
    //go through all event clips and init with instances 
    //enable the one shots

    //StartAmbiSystem
    //Play all instances 
    //disable the one shots


    //StopAmbiSystem
    //stop all instances
}
