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

    protected override void Attack() {
        SpawnRandomProjectile();
    }
    private void SpawnRandomProjectile() {
        attackComplete = false;
        RandomSpawnLoc();
        Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
        attackComplete = true;
    }
    private void RandomSpawnLoc() {
        int randomClampedPosY = GetRandomYPos();
        SetSpawnLoc(randomClampedPosY);
    }
}
