using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    private GameObject target;
    private Vector3 projectileSpawnLoc;
    [SerializeField] private float playFieldPosConstraint = 8f;

    private void Awake() {
        target = FindObjectOfType<PlayerMovement>().gameObject;
        projectileSpawnLoc = transform.position;
    }
    public void RandomSpawnLoc() {
        float randomClampedPosY = Random.Range(-playFieldPosConstraint, playFieldPosConstraint);
        //float clampedPositionY = Mathf.Clamp(valueToCheck, -playFieldPosConstraint, playFieldPosConstraint);
        projectileSpawnLoc = new Vector3(transform.position.x, randomClampedPosY, transform.position.z);
    }
    public void SpawnProjectile() {
        RandomSpawnLoc();
        Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
    }
}
