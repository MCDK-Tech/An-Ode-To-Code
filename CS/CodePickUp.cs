using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePickUp : MonoBehaviour
{
    public DestroyPlatform[] platformsArray;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            foreach(DestroyPlatform platform in platformsArray)
            {
                platform.DestroyablePlatform();
            }
        }
    }
}
