using System;
using UnityEngine;

public class Gun : Item
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Collider2D collider;

    private Vector3 initialLocalScale;

    private void Start()
    {
        initialLocalScale = transform.localScale;
    }


    void Update() 
    {
        if (Attached && Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public override void AttachToHand(Transform hand)
    {
        base.AttachToHand(hand);

        GetComponent<Rigidbody2D>().simulated = false;
        collider.enabled = false;
        transform.SetParent(hand, true);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public override void DetachFromHand()
    {
        base.DetachFromHand();
        GetComponent<Rigidbody2D>().simulated = true;
        collider.enabled = true;
        transform.parent = null;
        transform.localScale = initialLocalScale;
    }
}
