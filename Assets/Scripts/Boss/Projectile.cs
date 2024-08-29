using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] protected Rigidbody2D rb; 
    [SerializeField] protected float projectileSpeed = 6f;
    [SerializeField] protected float maxLifeTime = 3f;
    protected float timer;
    protected float projectileMultiplier = 1.5f;
    
    public virtual void FixedUpdate() {
        MoveProjectile();
        timer += Time.deltaTime;
        if (timer >= maxLifeTime) {
            Destroy(gameObject);
            timer = 0;
        }
    }
    public virtual void MoveProjectile() {
        rb.velocity = new Vector2(-120f, 0f) * projectileSpeed * Time.fixedDeltaTime;
    }


    public void ProjectileDeath()
    {
        //refactor to some type of explosion animation
        Destroy(this.gameObject);
    }
    public void SetProjectile(ProjectileSpeedUpgradeEnum projectileSpeedUpgradeEnum)
    {
        switch (projectileSpeedUpgradeEnum)
        {
            case ProjectileSpeedUpgradeEnum.NORMAL:
                //we chillin
                break;
            case ProjectileSpeedUpgradeEnum.HIGH_SPEED:
                projectileSpeed = projectileSpeed * projectileMultiplier;
                break;
            case ProjectileSpeedUpgradeEnum.LOW_SPEED:
                projectileSpeed = projectileSpeed / projectileMultiplier;
                break;
            default:
                break;
        }
    }
}
