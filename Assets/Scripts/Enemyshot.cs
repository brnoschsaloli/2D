using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Enemyshot : MonoBehaviour
{


    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private float fireRate;

    private float nextFireTime = 0f;


    private void FixedUpdate()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }


    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.SetDirection(-1);
    }

}