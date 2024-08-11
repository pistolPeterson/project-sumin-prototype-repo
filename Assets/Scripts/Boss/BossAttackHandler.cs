using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class BossAttackHandler : MonoBehaviour
{
    [SerializeField] private BossAttackHandlerData handlerData;
    [HideInInspector] public UnityEvent OnEncounterActive;
    [HideInInspector] public UnityEvent OnSpecialPerformed;
    [SerializeField] private bool skipSpecial = false;
    private AttackPattern currentAttack;

    [Header("Attacking Stats")]
    [SerializeField] private float encounterDuration = 60f;
    [SerializeField] private float percentageForSpecialMove = 0.25f;
    [field: SerializeField] public float encounterTimer { get; private set; }
    private bool encounterComplete = false;
    private bool specialPhaseActive = false;

    public List<AttackPattern> attackPatterns;
    //UPGRADES 
    [field: SerializeField] public ProjectileSpeedUpgradeEnum ProjectileSpeedState { get; set; } = ProjectileSpeedUpgradeEnum.NORMAL;
    
    private void Start()
    {
       var potentialAttacks = GetComponentsInChildren<AttackPattern>();
       foreach (var ap in potentialAttacks)
       {
           attackPatterns.Add(ap);
           ap.Initialize(this);
       }
        
        Listeners();
        StartEncounterAttacks();
    }
    private void Listeners() {
        if (handlerData.possibleAttackPatterns.Count > 0) {
            foreach (AttackPattern ap in handlerData.possibleAttackPatterns) {
                ap.OnCompleteAttack.AddListener(NextAttack);
            }
        }
        if (handlerData.possibleSpecialMoves.Count > 0) {
            foreach (AttackPattern sm in handlerData.possibleSpecialMoves) {
                sm.OnCompleteAttack.AddListener(NextAttack);
                sm.OnCompleteSpecial.AddListener(SpecialMoveDone);
            }
        }
    }
    [ProButton]
    public void StartEncounterAttacks() {
        StartCoroutine(StartEncounterTimer());
    }
    private void NextAttack() {
        if (encounterComplete) {
            return;
        }
        PerformAttack(handlerData.GetRandomAttack());
    }
    private void NextSpecial() {
        if (encounterComplete) {
            return;
        }
        Debug.Log("Special !");
        PerformSpecial(handlerData.GetRandomSpecial());
    }
    
    // ENCOUNTER LOOP:
    private IEnumerator StartEncounterTimer() {
        encounterComplete = false;
        encounterTimer = 0f;
        float intervalForSpecial = (encounterDuration * percentageForSpecialMove) - 0.5f; // slight offset so the UI doesnt look off
        int intervalCounter = 0;
        NextAttack();

        while (encounterTimer < encounterDuration) {
            encounterTimer += Time.deltaTime + 1f;
            OnEncounterActive.Invoke(); // this is for the encounter UI bar to update
            if (!skipSpecial && !specialPhaseActive && encounterTimer >= intervalForSpecial * (intervalCounter + 1)) {
                Debug.Log("Phase");
                specialPhaseActive = true;
                currentAttack?.StopAttack();
                NextSpecial();
                intervalCounter++;
            }
            if (encounterTimer >= intervalForSpecial * (intervalCounter + 1) + intervalForSpecial) {
                specialPhaseActive = false;
            }
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(SpecialPhaseNotActive); // to stop timer when special phase is active
        }        
        // this is for when the encounter is complete:
        currentAttack.StopAttack();
        encounterComplete = true;
        Debug.Log("Encounter complete");        
    }
    public bool SpecialPhaseNotActive() {
        return specialPhaseActive == false;
    }
    private void SpecialMoveDone() {
        specialPhaseActive = false;
    }
    public void PerformAttack(AttackPattern ap) {
        currentAttack = ap;
        currentAttack.StartAttack();
    }
    public void PerformSpecial(AttackPattern ap) {
        currentAttack = ap;
        currentAttack.StartAttack();
        OnSpecialPerformed.Invoke();
    }
    public float GetEncounterDuration() {
        return encounterDuration;
    }
}
