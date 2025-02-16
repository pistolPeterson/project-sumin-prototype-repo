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
        Debug.Log("Max Val: " + encounterBarSlider.maxValue);
    }
    public void UpdateEncounterBar() {
        encounterBarSlider.value = bossAttackHandler.encounterTimer;
        Debug.Log(encounterBarSlider.value);
    }

}
