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
        IsActivelyPlaying = true;
        canvasGroup.alpha = 0;
        dialogueTextUI.text = dialogue.Text;
        canvasGroup.DOFade(1, dialogue.GetAudioLength()).OnComplete(() =>
        {
            canvasGroup.DOFade(0, dialogue.GetAudioLength());
            IsActivelyPlaying = false;
        });
    }

    [ProButton]
    private void TestDialogue() => DisplayDialogue(TEST_DIALOGUE);
}