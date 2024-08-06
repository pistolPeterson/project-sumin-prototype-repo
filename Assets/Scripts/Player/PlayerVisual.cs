using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private PlayerHealth hp;
    [SerializeField] private PlayerImpactVisual impactVisual;
    [SerializeField] private Animator eye;
    [SerializeField] private Animator wings;

    [Header("Clips")]
    [SerializeField] private AnimationClip eyeUp;
    [SerializeField] private AnimationClip eyeDown;
    [SerializeField] private AnimationClip wingUp;
    [SerializeField] private AnimationClip wingDown;
    private void Start() {
        hp.OnHealthChange.AddListener(PlayImpact);
    }
    public void PlayImpact(int num) {
        if (num < 0) {
            impactVisual.PlayImpactClip();
        }
    }
    public void PlayUp() {
        eye.Play(eyeUp.name);
        wings.Play(wingUp.name);
    }
    public void PlayDown() {
        eye.Play(eyeDown.name);
        wings.Play(wingDown.name);
    }
}
