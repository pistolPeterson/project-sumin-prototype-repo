using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class BossAttackHandler : MonoBehaviour
{
    [SerializeField] private List<AttackPattern> attackPatterns;
    [SerializeField] private int attackIndex = 0;

    public void Attack() {
        attackPatterns[attackIndex].StartAttack();
    }

    
    [ProButton]
    private void StopAttack() {
        attackPatterns[attackIndex].StopAttack();
    }


}
