using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class BossAttackHandler : MonoBehaviour
{
    public List<AttackPattern> currentAttackPatterns = new List<AttackPattern>();
    public List<AttackPattern> currentSpecialMoves = new List<AttackPattern>();
    public List<AttackPattern> possibleAttackPatterns;
    public List<AttackPattern> possibleSpecialMoves;
    private AttackPattern currentAttack;
    
    [HideInInspector] public UnityEvent OnEncounterActive;
    [SerializeField] private bool skipSpecial = false;

    [Header("Attacking Stats")]
    [SerializeField] private float encounterDuration = 10f;
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
        if (possibleAttackPatterns.Count > 0) {
            foreach (AttackPattern ap in possibleAttackPatterns) {
                ap.OnCompleteAttack.AddListener(NextAttack);
            }
        }
        if (possibleSpecialMoves.Count > 0) {
            foreach (AttackPattern sm in possibleSpecialMoves) {
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
        PerformAttack(GetRandomAttack());
    }
    private void NextSpecial() {
        if (encounterComplete) {
            return;
        }
        Debug.Log("Special !");
        PerformSpecial(GetRandomSpecial());
    }
    private AttackPattern GetRandomAttack() {
        return currentAttackPatterns.OrderBy(x => Random.value).First(); // get the first element from a randomly ordered list
    }
    private AttackPattern GetRandomSpecial() {
        return currentSpecialMoves.OrderBy(x => Random.value).First();
    }
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
                currentAttack?.StopAttack();
                NextSpecial();
                specialPhaseActive = true;
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
    private bool SpecialPhaseNotActive() {
        return specialPhaseActive == false;
    }
    private void SpecialMoveDone() {
        specialPhaseActive = false;
    }
    [ProButton]
    public void PerformAttack(AttackPattern ap) {
        currentAttack = ap;
        currentAttack.StartAttack();
    }
    public void PerformSpecial(AttackPattern ap) {
        currentAttack = ap;
        currentAttack.StartAttack();
    }
    public void AddAttack(AttackPattern ap) {
        foreach (AttackPattern patterns in possibleAttackPatterns) {
            if (patterns == ap) {
                currentAttackPatterns.Add(ap);
            }
        }
    }
    public void AddSpecial(AttackPattern sm) {
        foreach (AttackPattern special in possibleSpecialMoves) {
            if (special == sm) {
                currentSpecialMoves.Add(sm);
            }
        }
    }
    public void RemoveAttack(AttackPattern ap) {
        if (currentAttackPatterns.Contains(ap)) {
            currentAttackPatterns.Remove(ap);
        }
        else Debug.LogWarning(ap + "attack not found: Nothing removed.");
    }
    public void RemoveSpecial(AttackPattern sm) {
        if (currentSpecialMoves.Contains(sm)) {
            currentSpecialMoves.Remove(sm);
        }
        else Debug.LogWarning(sm + " special not found: Nothing removed.");
    }
    public float GetEncounterDuration() {
        return encounterDuration;
    }
}
