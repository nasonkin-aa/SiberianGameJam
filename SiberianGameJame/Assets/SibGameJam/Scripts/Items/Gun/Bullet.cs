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
}
