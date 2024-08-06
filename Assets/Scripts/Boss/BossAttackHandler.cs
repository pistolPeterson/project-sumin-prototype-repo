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
    //public Dictionary<string, AttackPattern> currentAttackPatters = new Dictionary<string, AttackPattern>();
    //public Dictionary<string, AttackPattern> currentSpecialMoves = new Dictionary<string, AttackPattern>();
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
        NextAttack();
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
        return currentAttackPatterns.OrderBy(x => Random.value).First();
    }
    private AttackPattern GetRandomSpecial() {
        return currentSpecialMoves.OrderBy(x => Random.value).First();
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
