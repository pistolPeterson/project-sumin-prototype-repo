using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EncounterBarUI : MonoBehaviour
{
    [SerializeField] private BossAttackHandler bossAttackHandler;
    [SerializeField] private Slider encounterBarSlider;

    public void InitUI() {
        bossAttackHandler.OnEncounterActive.AddListener(UpdateEncounterBar);
        encounterBarSlider.maxValue = bossAttackHandler.GetEncounterDuration();
    }
    public void UpdateEncounterBar() {
        encounterBarSlider.value = bossAttackHandler.encounterTimer;
    }

}
