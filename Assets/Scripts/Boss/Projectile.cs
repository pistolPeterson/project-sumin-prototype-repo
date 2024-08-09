using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private float projectileSpeed = 6f;
    [SerializeField] private float maxLifeTime = 3f;
    private float timer;
    private float projectileMultiplier = 1.5f;
    
    private void FixedUpdate() {
        MoveProjectile();
        timer += Time.deltaTime;
        if (timer >= maxLifeTime) {
            Destroy(gameObject);
            timer = 0;
        }
    }
    private void MoveProjectile() {
        rb.velocity = new Vector2(-120f, 0f) * projectileSpeed * Time.fixedDeltaTime;
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
