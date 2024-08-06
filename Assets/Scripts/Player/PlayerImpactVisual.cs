using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactVisual : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip impactAnimationClip;
    
    public void PlayImpactClip() {
        animator.Play(impactAnimationClip.name);
    }
}
