
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueContainer : MonoBehaviour
{
   public List<Dialogue> subtitleList;

   public Dialogue GetRandomDialogue()
   {
       return subtitleList[Random.Range(0, subtitleList.Count)];
   }
  
  public void Play(Dialogue dialogueToPlay)
  {
    //randomly play dialogue 
    //show it in ui
    DialogueManager.Instance.DisplayDialogue(dialogueToPlay);
  }
  public void Play()
  {
      Debug.Log("playing dialogue");
      DialogueManager.Instance.DisplayDialogue(GetRandomDialogue());
  }
}
