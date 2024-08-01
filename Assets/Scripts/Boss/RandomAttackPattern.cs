using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttackPattern : AttackPattern
{
    public override void Attack() {
        SpawnRandomProjectile();
    }
    public void SpawnRandomProjectile() {
        RandomSpawnLoc();
        Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
    }
    public void RandomSpawnLoc() {
        float randomClampedPosY = Random.Range(-playFieldPosConstraint, playFieldPosConstraint);
        SetSpawnLoc(randomClampedPosY);
    }
}
