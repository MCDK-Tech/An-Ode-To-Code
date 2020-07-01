using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player_Movement Gun = other.GetComponent<Player_Movement>();

        if (other.CompareTag("Player"))
        {
            Gun.recievedGun = true;
            Destroy(gameObject);
        }
    }

    
}
