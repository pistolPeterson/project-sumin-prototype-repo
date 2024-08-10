using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalWallsAttackPattern : AttackPattern {
    
    [Header("Horizontal Wall Attack")]
    [SerializeField] private int projectilesPerWall = 5;
    [SerializeField] private float spaceInWall = 0.1f;
    protected override void Attack() {
        attackComplete = false;
        AttackLoop();
    }
    private void AttackLoop() {
        int avoid = GetRandomYPos();
        StartCoroutine(SpawnProjectiles(avoid));
    }
    private IEnumerator SpawnProjectiles(int yPosAvoid) {
        if (yPosAvoid % 2 != 0) {
            yPosAvoid++;
        }
        int projectilesSpawned = 0;
        while (projectilesSpawned < projectilesPerWall) {
            for (int yPos = (int)playFieldPosConstraint; yPos >= -playFieldPosConstraint; yPos -= 2) {
                if (yPos == yPosAvoid) continue;
                SetSpawnLoc(yPos);
                var projGO = Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
                SetProjectileSpeedState(projGO);
            }
            projectilesSpawned++;
            yield return new WaitForSeconds(spaceInWall);
        }
        attackComplete = true;
    }
}
