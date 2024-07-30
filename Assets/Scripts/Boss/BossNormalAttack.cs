using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    private GameObject target;

    private void Awake() {
        target = FindObjectOfType<PlayerMovement>().gameObject;
    }
    public void SpawnProjectile() {
        Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
    }
}
