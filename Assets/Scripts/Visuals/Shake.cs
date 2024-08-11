using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using com.cyborgAssets.inspectorButtonPro;

public class Shake : MonoBehaviour
{
    [SerializeField] private float duration = 0.1f;
    [SerializeField] private float strength = 0.5f;

    [ProButton]
    public void TriggerShake() {
        transform.DOShakePosition(duration, strength);
    }
}
