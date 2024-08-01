using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackPattern : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float playFieldPosConstraint = 8f;
    [SerializeField] protected float attackUptime = 5f; // how long an attack lasts for
    [SerializeField] protected float delayBetweenAttacks = 0.5f; // delay time before calling the next attack
    protected Vector3 projectileSpawnLoc;
    protected float uptimeTimer = 0f;

    private void Start() {
        projectileSpawnLoc = transform.position;
    }
    public abstract void Attack();
    public void SetSpawnLoc(float yPos) {
        projectileSpawnLoc = new Vector3(transform.position.x, yPos, transform.position.z);
    }
    public void StartAttack() {
        StartCoroutine(UptimeAttack());
    }
    public void StopAttack() {
        StopCoroutine(UptimeAttack());
        uptimeTimer = attackUptime;
    }
    private IEnumerator UptimeAttack() {
        uptimeTimer = 0f;
        while (uptimeTimer < attackUptime) {
            Debug.Log("Attacking Time remaining: " + (attackUptime - uptimeTimer));
            Attack();
            yield return new WaitForSeconds(delayBetweenAttacks);
            uptimeTimer += Time.fixedDeltaTime + delayBetweenAttacks; // to normalize the time
        }
    }
}
