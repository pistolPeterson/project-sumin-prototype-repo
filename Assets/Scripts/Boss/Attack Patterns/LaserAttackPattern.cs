using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttackPattern : AttackPattern {

    [Header("Laser Attack")]
    [SerializeField] private int projectilesPerLaser = 10;
    [SerializeField] private float delayBetweenProjSpawn = 0.1f;
    [SerializeField] private bool trackPlayerMovement = true;
    private Transform player;

    protected override void Start() {
        base.Start();
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }
    protected override void Attack() {
        SpawnLaser();
    }
    private void SpawnLaser() {
        attackComplete = false;
        RandomSpawnLoc();
        StartCoroutine(LaserShot());
    }
    private void RandomSpawnLoc() {
        int yPos;
        if (trackPlayerMovement)
            yPos = (int)player.position.y;
        else
            yPos = GetRandomYPos();
        SetSpawnLoc(yPos);
    }
    private IEnumerator LaserShot() {
        int projectilesSpawned = 0;
        while (projectilesSpawned < projectilesPerLaser) {
            var projGO = Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
            SetProjectileSpeedState(projGO);
            yield return new WaitForSeconds(delayBetweenProjSpawn);
            projectilesSpawned++;
        }
        attackComplete = true;
    }
}
