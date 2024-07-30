using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private float projectileSpeed = 6f;
    [SerializeField] private float maxLifeTime = 3f;
    private float timer;
    
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
}
