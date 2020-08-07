using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hitInfo) //When hitting collision
    {
        Player_Movement Gun = hitInfo.GetComponent<Player_Movement>(); //Gets player info

        if (hitInfo.CompareTag("Player")) //When hitting player sprite
        {
            Gun._HasGun = true; //Player has recieved gun
            Destroy(gameObject); //Destroy Gun gameobject
        }
    }
   
}
