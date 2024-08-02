using System;
using System.Collections;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class AttackPattern : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float playFieldPosConstraint = 8f;
    [SerializeField] private float attackDuration = 5f; // how long an attack lasts for
    [SerializeField] protected float delayBetweenAttacks = 0.5f; // delay time before calling the next attack
    protected Vector3 projectileSpawnLoc;
    private float timer = 0f;

    protected virtual void Start() {
        projectileSpawnLoc = transform.position;
        
    }
    public abstract void Attack();
    public void SetSpawnLoc(float yPos) {
        projectileSpawnLoc = new Vector3(transform.position.x, yPos, transform.position.z);
    }
    public void StartAttack() {
       
        StartCoroutine(AttackLoop());
    }
    [ProButton]
    private void StopAttack() {
        StopCoroutine(AttackLoop());
        timer = attackDuration;
    }
    
    private IEnumerator AttackLoop() {
        timer = 0f;
      
        while (timer < attackDuration) {
            //Debug.Log("Attacking Time remaining: " + (attackDuration - timer));
            Attack();
            yield return new WaitForSeconds(delayBetweenAttacks);
            timer += Time.fixedDeltaTime + delayBetweenAttacks; // to normalize the time
        }
    }
}
