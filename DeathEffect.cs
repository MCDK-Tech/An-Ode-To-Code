using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public float DeathTimer = 0.3f; //Animation Timer
    
    void Update()
    {
        Destroy(gameObject, DeathTimer); //Destroy after animation is done
    }
}
