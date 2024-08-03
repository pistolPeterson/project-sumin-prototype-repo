using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInTheWallAttackPattern : AttackPattern {
    
    [Header("Hole In The Wall Attack")]
    [SerializeField] private int wallsPerAttack = 5;
    protected override void Attack() {
        HoleAttack();
    }
    private void HoleAttack() {
        attackComplete = false;
        AttackLoop();
    }
    private void AttackLoop() {
        int avoid = GetRandomYPos();
        SpawnProjectiles(avoid);
    }
    private void SpawnProjectiles(int yPosAvoid) {
        if (yPosAvoid % 2 != 0) {
            yPosAvoid++;
        }
        Debug.Log(yPosAvoid);
        for (int yPos = (int)playFieldPosConstraint; yPos >= -playFieldPosConstraint; yPos-= 2) {
            if (yPos == yPosAvoid) continue;
            Debug.Log("Spawned: " + yPos);
            SetSpawnLoc(yPos);
            Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
        }
        attackComplete = true;
    }
}
