using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform Firepoint;
    public GameObject Bullet;
    public Player_Movement PM;
    public double shootTimer = 0.4;
    private double tracker;


    void Start()
    {
        tracker = shootTimer;
    }

    void Update()
    {
        
        shootTimer += Time.deltaTime;       
        if (shootTimer > tracker)
        {
            Shoot();
        }

    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && PM.canShoot())
        {
            shootTimer = 0;
            Instantiate(Bullet, Firepoint.position, Firepoint.rotation);
            
        }
    }

}