using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedAttackPattern : AttackPattern {

    [Header("Curved Attack")]
    [SerializeField] private List<GameObject> angledProjectilesPrefabs;
    protected override void Attack() {
        attackComplete = false;
        SpawnRandomProjectile();
    }
    private void SpawnRandomProjectile() {
        attackComplete = false;
        RandomSpawnLoc();
        ChooseRandomProjectile();
        GameObject projGO = Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
        SetProjectileSpeedState(projGO);
        attackComplete = true;
    }
    private void RandomSpawnLoc() {
        int randomClampedPosY = GetRandomYPos();
        SetSpawnLoc(randomClampedPosY);
    }
    private void ChooseRandomProjectile() {
        int random = Random.Range(0, angledProjectilesPrefabs.Count);
        projectilePrefab = angledProjectilesPrefabs[random];
    }
}
