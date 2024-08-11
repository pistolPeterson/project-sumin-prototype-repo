using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactVisual : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip impactAnimationClip;
    [SerializeField] private Shake shake;
    public void PlayImpact() {
        animator.Play(impactAnimationClip.name);
        shake.TriggerShake();
    }
}
