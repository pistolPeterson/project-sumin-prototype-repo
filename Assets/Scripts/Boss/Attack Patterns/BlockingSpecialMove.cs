using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockingSpecialMove : AttackPattern 
{

    [Header("Blocking Special")]
    [SerializeField] private int projectilesPerLaser = 10;
    [SerializeField] private float delayBetweenProjSpawn = 0.1f;
    private Vector3 projectileSpawnLoc2;
    private int lasersSpawned;
    protected override void Attack() {
        attackComplete = false;
        DoubleLaser();
    }
    private void DoubleLaser() {
        lasersSpawned = 0;
        RandomSpawnLoc();
        StartCoroutine(LaserShot(projectileSpawnLoc));
        StartCoroutine(LaserShot(projectileSpawnLoc2));
    }
    private void RandomSpawnLoc() {
        int yPos1 = GetRandomYPos();
        var randomYPositions = Enumerable.Range(-8, 17).Where(a => a != yPos1 && a % 2 == 0).ToArray();
        int yPos2 = randomYPositions[Random.Range(0, randomYPositions.Length)];
        SetSpawnLoc(yPos1);
        projectileSpawnLoc2 = new Vector3(transform.position.x, yPos2, transform.position.z);
    }
    private IEnumerator LaserShot(Vector3 spawnLoc) {
        int projectilesSpawned = 0;
        while (projectilesSpawned < projectilesPerLaser) {
          var projectileGO =  Instantiate(projectilePrefab, spawnLoc, Quaternion.identity);
          SetProjectileData(projectileGO);
            yield return new WaitForSeconds(delayBetweenProjSpawn);
            projectilesSpawned++;
        }
        lasersSpawned++;
        if (lasersSpawned == 2) {
            attackComplete = true;
        }
    }

   
}
