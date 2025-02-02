using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    private CanvasGroup canvasGroup;
    private TextMeshProUGUI dialogueTextUI;
    private Dialogue currentDialogue;

    public bool IsActivelyPlaying { get; private set; } = false;

    public Dialogue TEST_DIALOGUE;

    protected override void Awake()
    {
        base.Awake();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        dialogueTextUI = GetComponentInChildren<TextMeshProUGUI>();
        if(TEST_DIALOGUE)
            TestDialogue();
    }


    public void DisplayDialogue(Dialogue dialogue)
    {
        // Stop the current dialogue if playing
        if (IsActivelyPlaying)
        {
            currentDialogue.StopAudio(); 
            canvasGroup.DOKill(); 
            canvasGroup.alpha = 0; 
        }

        currentDialogue = dialogue;
        IsActivelyPlaying = true;
        canvasGroup.alpha = 0;
        dialogueTextUI.text = dialogue.Text;
        dialogue.PlayAudio();
        var lengthOfDialogueClip = dialogue.GetAudioLength();
        canvasGroup.DOFade(1, lengthOfDialogueClip + 0.2f).OnComplete(() =>
        {
            canvasGroup.DOFade(0, lengthOfDialogueClip);
            IsActivelyPlaying = false;
        });
    }

    [ProButton]
    private void TestDialogue() => DisplayDialogue(TEST_DIALOGUE);
}