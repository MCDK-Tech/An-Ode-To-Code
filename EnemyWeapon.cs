using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Transform Firepoint; //Location of Firepoint
    public GameObject Bullet; //Bullet Gameobject
    public Transform FireCheck; //Location of FireCheck
    public Vector2 FireChecksize; //FireCheck Size

    public double ShootInterval = 0.4; //Wait period between shooting
    private double _Tracker; //Tracks shoot interval

    void Start()
    { 
        _Tracker = ShootInterval; //Tracks shoot interval
    }

    void Update()
    {
        ShootInterval += Time.deltaTime; //Shoot interval update

        if (FireCheckBool() && ShootInterval > _Tracker) //If Enemy can shoot
        {
            Shoot();
        }
    }

    void Shoot() //Controls enemy 
    {
        ShootInterval = 0; //Interval starts once shot
        Instantiate(Bullet, Firepoint.position, Firepoint.rotation); //Bullet appears
    }

    public bool FireCheckBool() //Checks if enemy can shoot
    {
        return Physics2D.OverlapBox(FireCheck.position, FireChecksize, 0, LayerMask.GetMask("Player"));
    }

    private void OnDrawGizmosSelected() //Draws FireCheck
    {
        Gizmos.DrawWireCube(FireCheck.position, FireChecksize);
    }
}
