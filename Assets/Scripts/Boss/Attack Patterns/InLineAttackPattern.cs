using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLineAttackPattern : AttackPattern
{
    [Header("In Line Attack")]
    [SerializeField] private float delayBetweenProjSpawn = 0.1f;

    protected override void Start() {
        base.Start();
    }
    protected override void Attack() {
        SpawnInLineProjectile();
    }
    private void SpawnInLineProjectile() {
        attackComplete = false;
        StartCoroutine(DownLineAttack());
    }
    private IEnumerator DownLineAttack() {
        int currPos = (int)playFieldPosConstraint;
        while (currPos > -(int)playFieldPosConstraint) {
            SetSpawnLoc(currPos);
            GameObject projGO = Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity); 
            SetProjectileSpeedState(projGO);
            // horizontal projectiles
            SetSpawnLoc(playFieldPosConstraint); // top
             projGO = Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
            SetProjectileSpeedState(projGO);

            SetSpawnLoc(-playFieldPosConstraint); // bot
             projGO = Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
            SetProjectileSpeedState(projGO);
            yield return new WaitForSeconds(delayBetweenProjSpawn);
            if (currPos == playFieldPosConstraint - 4)
                attackComplete = true;
            currPos--;
        }
    }
}
