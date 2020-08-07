using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePickUp : MonoBehaviour
{
    public DestroyPlatform[] platformsArray;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == ("Player")) //If CodePickUp hits Player
        {
            Destroy(gameObject); //Destroys CodePickUp

            foreach(DestroyPlatform platform in platformsArray)
            {
                platform.DestroyablePlatform(); //Destroys platform linked to pickup
            }
        }
    }
}
