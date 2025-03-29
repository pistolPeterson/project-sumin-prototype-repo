using System;
using FMODUnity;
using PeteUnityUtils.MinMaxSlider;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[Serializable]
public class AmbientOneShot
{
    public EventReference eventClip;
    public bool IsEnabled { get; set; } = false;
    [MinMaxSlider(5f, 25f)]
    public Vector2 waitTimeRange = new(5f, 25f);

    private float lastWaitTime = -1f;

    public float GetLastWaitTime() => lastWaitTime;
    public void SetRandomWaitTime(float addedTime)
    {
        lastWaitTime = Random.Range(waitTimeRange.x, waitTimeRange.y) + addedTime;
    }
}
