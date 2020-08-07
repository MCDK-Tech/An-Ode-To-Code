using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float BulletSpeed = 20f; //Bullet Speed
    public int BulletDamage = 20; //Bullet Damage
    public float BulletLongevity = 0.5f; //Bullet longevity

    public Rigidbody2D rbBullet; //Rigidbody of Bullet
    public GameObject DeathEffect; //Gameobject DeathEffect

    void Start()
    {
        rbBullet.velocity = transform.right * -BulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Player_Movement Player = hitInfo.GetComponent<Player_Movement>(); //Contacted Player information

        if (Player != null) //When Player is there
        {
            Player.TakeDamage(BulletDamage); //Player takes damage
        }

        if (hitInfo.gameObject.tag == ("Ground") || hitInfo.gameObject.tag == ("Player")) //When Bullet hits Player or Ground
        {
            Instantiate(DeathEffect, transform.position, transform.rotation); //Trigger DeathEffect
            Destroy(gameObject); //Destroy Bullet
        }
    }

    private void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject, BulletLongevity); //Bullet destroys itself after a period of time
    }
}
