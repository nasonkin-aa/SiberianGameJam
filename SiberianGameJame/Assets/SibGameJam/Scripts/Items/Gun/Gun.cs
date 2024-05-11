using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update() 
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
            AudioManager.instance.PlayPlasmaGun();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
