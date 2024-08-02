using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackHandler : MonoBehaviour
{
    [SerializeField] private List<AttackPattern> attackPatterns;
    [SerializeField] private int attackIndex = 0;

    public void Attack() {
        attackPatterns[attackIndex].StartAttack();
    }


}
