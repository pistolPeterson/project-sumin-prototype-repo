using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PulseAlphaEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bossEyeSprite; 
    [SerializeField] private SpriteRenderer bossWingSprite;
    [SerializeField] private float pulseDuration = 1.0f;
    [SerializeField] private float minAlpha = 0.01f;
    [SerializeField]  private float maxAlpha = 0.25f;

    private void Start()
    {
        PulseAlpha(bossEyeSprite);
        PulseAlpha(bossWingSprite);

    }

    private void PulseAlpha(SpriteRenderer sr)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(sr.DOFade(minAlpha, pulseDuration / 2));
        sequence.Append(sr.DOFade(maxAlpha, pulseDuration / 2));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }



}
