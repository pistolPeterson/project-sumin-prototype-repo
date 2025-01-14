using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// data file to hold audio and the relevant text
/// </summary>

[CreateAssetMenu(fileName = "Dialogue Instance", menuName = "Create Dialogue Object")]
public class Dialogue : ScriptableObject
{
     [TextAreaAttribute] public string DialogueText = "wow! you found super secret dialogue!";
     public AudioClip clip;

     public float GetAudioLength()
     {
          //Temporary length clip converter
          return DialogueText.Length * Random.Range(2.0f, 4.0f);
         // return clip != null ? clip.length : 1.0f;
     }
}
