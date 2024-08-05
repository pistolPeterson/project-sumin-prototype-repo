using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using System.Linq;

public class BossAttackHandler : MonoBehaviour
{
    public Dictionary<string, AttackPattern> currentAttackPatterns = new Dictionary<string, AttackPattern>();
    public Dictionary<string, SpecialMove> currentSpecialMoves = new Dictionary<string, SpecialMove>();
    [SerializeField] private List<AttackPattern> possibleAttackPatterns;
    [SerializeField] private List<SpecialMove> possibleSpecialMoves;
    private AttackPattern currentAttack;
    private SpecialMove currentSpecial;

    [Header("Attacking Stats")]
    [SerializeField] private float encounterDuration = 10f;
    [SerializeField] private float percentageForSpecialMove = 0.25f;
    public  float encounterTimer = 0f;
    private bool encounterComplete = false;

    private void Start() {
        // Default Attack
        Listeners();
        AddAttack(possibleAttackPatterns[0].name);
        AddAttack(possibleAttackPatterns[2].name);
        AddAttack(possibleAttackPatterns[4].name);
        StartEncounterAttacks();
        // Default Special
       // AddSpecial(possibleSpecialMoves[0].name);
    }
    private void Listeners() {
        if (possibleAttackPatterns.Count > 0) {
            foreach (AttackPattern ap in possibleAttackPatterns) {
                ap.OnCompleteAttack.AddListener(NextAttack);
            }
        }
        if (possibleSpecialMoves.Count > 0) {
            foreach (SpecialMove sm in possibleSpecialMoves) {
                sm.OnCompleteSpecial.AddListener(NextAttack);
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
        Debug.Log("Next Attack");
        string nextAttack = GetRandomAttack();
        PerformAttack(nextAttack);
    }
    private string GetRandomAttack() {
        string randomAttack = currentAttackPatterns.OrderBy(x => Random.value).First().Value.name;
        return randomAttack;
    }
    private IEnumerator StartEncounterTimer() {
        encounterComplete = false;
        encounterTimer = 0f;

        while (encounterTimer < encounterDuration) {
            encounterTimer += Time.deltaTime + 1f;
            yield return new WaitForSeconds(1f);
        }        
        currentAttack.StopAttack();
        encounterComplete = true;
        Debug.Log("Encounter complete");        
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
        currentSpecial = currentSpecialMoves[nameOfSpecial];
        currentSpecial.StartAttack();
    }
    private void StopAttack() {
        currentAttack.StopAttack();
    }
    public void AddAttack(string nameOfAttack) {
        foreach (AttackPattern ap in possibleAttackPatterns) {
            if (ap.name.Equals(nameOfAttack)) {
                currentAttackPatterns.Add(ap.name, ap);
            }
        }
    }
    public void AddSpecial(string nameOfSpecial) {
        foreach (SpecialMove sm in possibleSpecialMoves) {
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
}
