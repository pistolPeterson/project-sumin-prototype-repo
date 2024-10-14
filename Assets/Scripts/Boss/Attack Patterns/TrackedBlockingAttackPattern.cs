using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrackedBlockingAttackPattern : AttackPattern {

    [Header("Blocking Special")]
    [SerializeField] private GameObject laserProjectile;
    [SerializeField] private int projectilesPerBlocker = 10;
    [SerializeField] private int projectilesPerlaser = 5;
    [SerializeField] private float delayBetweenProjSpawn = 0.1f;
    [SerializeField] private float delayBetweenlaserProjSpawn = 0.1f;
    [SerializeField] private bool trackPlayerMovement = true;
    private Vector3 projectileSpawnLoc2;
    private Vector3 laserSpawnLoc;
    private int lasersSpawned;    
    private Transform player;

    protected override void Start() {
        base.Start();
        player = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }
    protected override void Attack() {
        attackComplete = false;
        DoubleLaser();
    }
    private void DoubleLaser() {
        lasersSpawned = 0;
        BlockerSpawnLoc();
        LaserSpawnLoc();
        StartCoroutine(BlockerShot(projectileSpawnLoc));
        StartCoroutine(BlockerShot(projectileSpawnLoc2));
        StartCoroutine(LaserShot());
    }
    private void BlockerSpawnLoc() {
        int yPos1 = GetRandomYPos();
        var randomYPositions = Enumerable.Range(-8, 17).Where(a => a != yPos1 && a % 2 == 0).ToArray(); 
        int yPos2 = randomYPositions[Random.Range(0, randomYPositions.Length)];
        SetSpawnLoc(yPos1);
        projectileSpawnLoc2 = new Vector3(transform.position.x, yPos2, transform.position.z);
    }
    private void LaserSpawnLoc() {
        if (trackPlayerMovement)
            laserSpawnLoc = new Vector3(transform.position.x, player.position.y, transform.position.z);
        else {
            var randomYPositions = Enumerable.Range(-8, 17).Where(a => a != projectileSpawnLoc.y && a != projectileSpawnLoc2.y && a % 2 == 0).ToArray();
            // list of possible y positions excluding odd numbers and the positions of the other projectiles
            int yPos2 = randomYPositions[Random.Range(0, randomYPositions.Length)];
            laserSpawnLoc = new Vector3(transform.position.x, yPos2, transform.position.z);
        }
    }
    private IEnumerator BlockerShot(Vector3 spawnLoc) {
        int projectilesSpawned = 0;
        while (projectilesSpawned < projectilesPerBlocker) {
            var projGO = Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
            SetProjectileData(projGO);
            yield return new WaitForSeconds(delayBetweenProjSpawn);
            projectilesSpawned++;
        }
        lasersSpawned++;
        if (lasersSpawned == 3) {
            attackComplete = true;
        }
    }
    private IEnumerator LaserShot() {
        int projectilesSpawned = 0;
        while (projectilesSpawned < projectilesPerlaser) {
            var projGO = Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
            SetProjectileData(projGO);
            yield return new WaitForSeconds(delayBetweenlaserProjSpawn);
            projectilesSpawned++;
        }
        lasersSpawned++;
        if (lasersSpawned == 3) {
            attackComplete = true;
        }
    }

}
