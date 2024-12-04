using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BossAttackHandlerData : MonoBehaviour
{
    public List<AttackPattern> currentAttackPatterns = new List<AttackPattern>();
    public List<AttackPattern> currentSpecialMoves = new List<AttackPattern>();
    public List<AttackPattern> possibleAttackPatterns;
    public List<AttackPattern> possibleSpecialMoves;

    public AttackPattern GetRandomAttack() {
        return currentAttackPatterns.OrderBy(x => Random.value).First(); // get the first element from a randomly ordered list
    }
    public AttackPattern GetRandomSpecial() {
        return currentSpecialMoves.OrderBy(x => Random.value).First();
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
       
    }
    public void RemoveSpecial(AttackPattern sm) {
        if (currentSpecialMoves.Contains(sm)) {
            currentSpecialMoves.Remove(sm);
        }
       
    }
}
