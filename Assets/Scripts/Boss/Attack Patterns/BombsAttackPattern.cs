using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombsAttackPattern : AttackPattern {

    [Header("Bombs Attack")]
    [SerializeField] private bool trackPlayerMovement;
    [SerializeField] private float spawnOffsetX = 2f;
    private Transform player;

    protected override void Start() {
        base.Start();
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }
    protected override void Attack() {
        attackComplete = false;
        SpawnBomb();
    }
    private void SpawnBomb() {
        if (trackPlayerMovement)
            projectileSpawnLoc = new Vector3(player.position.x + spawnOffsetX, player.position.y, 0);
        else {
            float randomY = GetRandomYPos();
            float randomX = GetRandomYPos();
            projectileSpawnLoc = new Vector3(randomX, randomY, 0);
        }
        var projGO =Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
        SetProjectileSpeedState(projGO);
        attackComplete = true;
    }
}
