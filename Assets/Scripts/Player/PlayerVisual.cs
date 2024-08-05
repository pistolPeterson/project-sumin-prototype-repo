using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private PlayerImpactVisual impactVisual;

    public void PlayImpact() {
        impactVisual.PlayImpactClip();
    }
}
