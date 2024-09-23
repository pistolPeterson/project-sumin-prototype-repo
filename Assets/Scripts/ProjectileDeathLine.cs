using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDeathLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Projectile>(out Projectile projectile))
        {
            projectile.ProjectileDeath();
        }
    }
}
