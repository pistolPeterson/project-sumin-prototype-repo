using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// data file to hold audio and the relevant text
/// </summary>

[CreateAssetMenu(fileName = "Dialogue Instance", menuName = "Create Dialogue Object")]
public class Dialogue : ScriptableObject
{
     [FormerlySerializedAs("DialogueText")] [TextAreaAttribute] public string Text = "wow! you found super secret dialogue!";
     public AudioClip clip;

     public float GetAudioLength()
     {
          //Temporary length clip converter
          return Text.Length * Random.Range(0.05f, 0.1f);
         // return clip != null ? clip.length : 1.0f;
     }
}
