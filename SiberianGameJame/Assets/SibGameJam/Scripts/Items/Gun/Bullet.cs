using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Item
{
    public float speed = 20;
    public Rigidbody2D rb;

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TryTakeDamage(1);
            Destroy(gameObject);
        }
    }
}
