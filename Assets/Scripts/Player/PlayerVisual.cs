using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private PlayerHealth hp;
    [SerializeField] private PlayerImpactVisual impactVisual;
    private void Start() {
        hp.OnHealthChange.AddListener(PlayImpact);
    }
    public void PlayImpact(int num) {
        if (num < 0) {
            impactVisual.PlayImpactClip();
        }
    }
}
