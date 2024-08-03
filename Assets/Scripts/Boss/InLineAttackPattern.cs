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
            Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
            SetSpawnLoc(playFieldPosConstraint);
            Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
            SetSpawnLoc(-playFieldPosConstraint);
            Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
            yield return new WaitForSeconds(delayBetweenProjSpawn);
            if (currPos == playFieldPosConstraint / 2)
                attackComplete = true;
            currPos--;
        }
    }
}
