using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    private GameObject target;
    private Vector3 projectileSpawnLoc;
    [SerializeField] private float playFieldPosConstraint = 8f;
    private int currPos;
    private void Awake() {
        target = FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void Start() {
        projectileSpawnLoc = transform.position;
        currPos = (int)playFieldPosConstraint;
    }
    public void SpawnInLineProjectile() {
        InLinePatternAttack();
        Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
    }
    public void SpawnRandomProjectile() {
        RandomSpawnLoc();
        Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
    }
    public void SetSpawnLoc(float yPos) {
        projectileSpawnLoc = new Vector3(transform.position.x, yPos, transform.position.z);
    }
    public void RandomSpawnLoc() {
        float randomClampedPosY = Random.Range(-playFieldPosConstraint, playFieldPosConstraint);
        SetSpawnLoc(randomClampedPosY);
    }
    public void InLinePatternAttack() {
        SetSpawnLoc(currPos);
        currPos--;
        if (currPos < -playFieldPosConstraint) {
            currPos = (int)playFieldPosConstraint;
        }
    }
}
