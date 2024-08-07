using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInTheWallAttackPattern : AttackPattern {
    
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
        for (int yPos = (int)playFieldPosConstraint; yPos >= -playFieldPosConstraint; yPos-= 2) {
            if (yPos == yPosAvoid) continue;
            SetSpawnLoc(yPos);
            Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
        }
        attackComplete = true;
    }
}
