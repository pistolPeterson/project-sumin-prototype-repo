using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomAttackPattern : AttackPattern
{
    protected override void Start()
    {
        base.Start();
    }

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
