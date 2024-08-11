using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackVisuals : MonoBehaviour
{
    [SerializeField] private BossAttackHandler handler;
    [SerializeField] private BossWingsAnimation wingsAnimation;
    [SerializeField] private Shake shake;
    [SerializeField] private float rotationSpeedMultiplier = 2;
    private void Start() {
        handler.OnSpecialPerformed.AddListener(TriggerAnimations);
    }
    public void TriggerAnimations() {
        StartCoroutine(SpecialMoveAnimations());
    }
    public IEnumerator SpecialMoveAnimations() {
        shake.TriggerShake();
        var originalSpeed = wingsAnimation.speedMultiplier;
        while (!handler.SpecialPhaseNotActive()) {
            wingsAnimation.speedMultiplier = rotationSpeedMultiplier;
            yield return null;
        }
        if (handler.SpecialPhaseNotActive()) wingsAnimation.speedMultiplier = originalSpeed;
    }
}
