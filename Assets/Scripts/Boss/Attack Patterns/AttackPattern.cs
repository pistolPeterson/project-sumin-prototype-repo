using System;
using System.Collections;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class AttackPattern : MonoBehaviour
{
    protected float playFieldPosConstraint = 8f;
    [HideInInspector] public UnityEvent OnCompleteAttack; // when the entire attack loop is complete
    [HideInInspector] public UnityEvent OnCompleteSpecial; // when the entire special loop is complete
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] private float attackDuration = 5f; // how long an attack lasts for
    [SerializeField] protected float delayBetweenAttacks = 0.5f; // delay time before calling the next attack
    protected Vector3 projectileSpawnLoc;
    protected bool attackComplete = false;
    protected bool attackForceStopped = false;
    [SerializeField] protected bool isSpecialMove = false;
    private float timer = 0f;
    protected BossAttackHandler bossAttackHandler;
    protected virtual void Start() {
        projectileSpawnLoc = transform.position;        
    }
    protected abstract void Attack();
    protected void SetSpawnLoc(float yPos) {
        projectileSpawnLoc = new Vector3(transform.position.x, yPos, transform.position.z);
    }
    public void StartAttack() {
        attackForceStopped = false;
        StartCoroutine(AttackLoop());
    }
   
    public void StopAttack() {
        timer = attackDuration;
        attackForceStopped = true;
        Debug.Log("Attack stopped");
    }
    protected int GetRandomYPos() {
        int randomPosY = (int)UnityEngine.Random.Range(-playFieldPosConstraint - 1, playFieldPosConstraint + 1); // +-1 to include constraint
        if (randomPosY % 2 != 0) {
            randomPosY++;
        }
        return randomPosY;
    }
    
    private IEnumerator AttackLoop() {
        timer = 0f;
        while (timer < attackDuration) {
            Attack();
            yield return new WaitUntil(IsAttackComplete);
            yield return new WaitForSeconds(delayBetweenAttacks);
            timer += Time.fixedDeltaTime + delayBetweenAttacks; // to normalize the time
        }
        yield return new WaitUntil(IsAttackComplete);
        if (!attackForceStopped) // only if this has not been forced stopped
                                 // This is for the special. So that the next attack is not called
            OnCompleteAttack.Invoke(); // invoke to start next attack
        if (isSpecialMove) OnCompleteSpecial.Invoke();
    }
    public bool IsAttackComplete() {
        return attackComplete;
    }
    public float GetAttackDuration() {
        return attackDuration;
    }

    public void Initialize(BossAttackHandler bossAttackHandler) //can also set up listenerrs with this too
    {
        this.bossAttackHandler = bossAttackHandler;
    }
    
    protected void SetProjectileSpeedState(GameObject projectileGO)
    {
        if (bossAttackHandler.ProjectileSpeedState == ProjectileSpeedUpgradeEnum.NORMAL)
            return; //rejected, how tragic
        AngledProjectile angled = projectileGO.GetComponent<AngledProjectile>();
        if (angled) {
            angled.SetProjectile(bossAttackHandler.ProjectileSpeedState);
        }
        else {
            projectileGO.GetComponent<Projectile>().SetProjectile(bossAttackHandler.ProjectileSpeedState);
        }

    }
}
