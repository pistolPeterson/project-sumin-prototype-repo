using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class BossAttackHandler : MonoBehaviour
{
    public Dictionary<string, AttackPattern> currentAttackPatterns = new Dictionary<string, AttackPattern>();
    public Dictionary<string, AttackPattern> currentSpecialMoves = new Dictionary<string, AttackPattern>();
    [SerializeField] private List<AttackPattern> possibleAttackPatterns;
    [SerializeField] private List<AttackPattern> possibleSpecialMoves;
    public AttackPattern currentAttack;
    [HideInInspector] public UnityEvent OnEncounterActive;

    [Header("Attacking Stats")]
    [SerializeField] private float encounterDuration = 10f;
    [SerializeField] private float percentageForSpecialMove = 0.25f;
    [field: SerializeField] public float encounterTimer { get; private set; }
    private bool encounterComplete = false;
    private bool specialPhaseActive = false;

    private void Start() {
        // Default Attack
        Listeners();
        AddAttack(possibleAttackPatterns[0].name);
        AddAttack(possibleAttackPatterns[1].name);
        AddAttack(possibleAttackPatterns[2].name);
        AddAttack(possibleAttackPatterns[3].name);
        AddAttack(possibleAttackPatterns[4].name);
        AddAttack(possibleAttackPatterns[5].name);
        StartEncounterAttacks();
        // Default Special
        AddSpecial(possibleSpecialMoves[0].name);
        AddSpecial(possibleSpecialMoves[1].name);
        // AddSpecial(possibleSpecialMoves[0].name);
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
    public void AddStartingAttacks(List<AttackPattern> attackPatterns, List<AttackPattern> specialMoves) {
        foreach (AttackPattern ap in attackPatterns) {
            AddAttack(ap.name);
        }
        foreach (AttackPattern sm in specialMoves) {
            AddSpecial(sm.name);
        }
    }
    [ProButton]
    public void StartEncounterAttacks() {
        StartCoroutine(StartEncounterTimer());
        NextAttack();
    }
    private void NextAttack() {
        if (encounterComplete) {
            return;
        }
        string nextAttack = GetRandomAttack();
        PerformAttack(nextAttack);
    }
    private void NextSpecial() {
        if (encounterComplete) {
            return;
        }
        Debug.Log("Special !");
        string nextSpecial = GetRandomSpecial();
        PerformSpecial(nextSpecial);
    }
    private string GetRandomAttack() {
        string randomAttack = currentAttackPatterns.OrderBy(x => Random.value).First().Value.name;
        return randomAttack;
    }
    private string GetRandomSpecial() {
        string randomSpecial = currentSpecialMoves.OrderBy(x => Random.value).First().Value.name;
        return randomSpecial;
    }
    private IEnumerator StartEncounterTimer() {
        encounterComplete = false;
        encounterTimer = 0f;
        float intervalForSpecial = encounterDuration * percentageForSpecialMove;
        int intervalCounter = 0;

        while (encounterTimer < encounterDuration) {
            encounterTimer += Time.deltaTime + 1f;
            OnEncounterActive.Invoke();
            if (!specialPhaseActive && encounterTimer >= intervalForSpecial * (intervalCounter + 1)) {
                currentAttack?.StopAttack();
                NextSpecial();
                specialPhaseActive = true;
                intervalCounter++;
            }
            if (encounterTimer >= intervalForSpecial * (intervalCounter + 1) + intervalForSpecial) {
                specialPhaseActive = false;
            }
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(PhaseNotActive);
        }        
        currentAttack.StopAttack();
        encounterComplete = true;
        Debug.Log("Encounter complete");        
    }
    private bool PhaseNotActive() {
        return specialPhaseActive == false;
    }
    private void SpecialMoveDone() {
        specialPhaseActive = false;
    }
    [ProButton]
    public void PerformAttack(int index) {
        PerformAttack(possibleAttackPatterns[index].name);
    }
    public void PerformAttack(string nameOfAttack) {
        currentAttack = currentAttackPatterns[nameOfAttack];
        currentAttack.StartAttack();
    }
    public void PerformSpecial(int index) {
        PerformSpecial(possibleSpecialMoves[index].name);
    }
    public void PerformSpecial(string nameOfSpecial) {
        currentAttack = currentSpecialMoves[nameOfSpecial];
        currentAttack.StartAttack();
    }
    public void AddAttack(string nameOfAttack) {
        foreach (AttackPattern ap in possibleAttackPatterns) {
            if (ap.name.Equals(nameOfAttack)) {
                currentAttackPatterns.Add(ap.name, ap);
            }
        }
    }
    public void AddSpecial(string nameOfSpecial) {
        foreach (AttackPattern sm in possibleSpecialMoves) {
            if (sm.name.Equals(nameOfSpecial)) {
                currentSpecialMoves.Add(sm.name, sm);
            }
        }
    }
    public void RemoveAttack(string nameOfAttack) {
        if (currentAttackPatterns.ContainsKey(nameOfAttack)) {
            currentAttackPatterns.Remove(nameOfAttack);
        }
        else Debug.LogWarning("attack not found: Nothing removed.");
    }
    public void RemoveSpecial(string nameOfSpecial) {
        if (currentSpecialMoves.ContainsKey(nameOfSpecial)) {
            currentSpecialMoves.Remove(nameOfSpecial);
        }
        else Debug.LogWarning("special not found: Nothing removed.");
    }
    public float GetEncounterDuration() {
        return encounterDuration;
    }
}
