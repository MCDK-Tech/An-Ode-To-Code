using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public Rigidbody2D rb;
    public GameObject DeathEffect;
    public float Erase_Bullet = 0.5f;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        if (hitInfo.gameObject.tag==("Ground") || hitInfo.gameObject.tag == ("Enemy"))
        {
            Instantiate(DeathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }   

   


    private void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject, Erase_Bullet);
    }

    void Update()
    {
        
    }
}
