using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public float Death_Timer = 0.3f;
    void Start()
    {

    }


    void Update()
    {
        Destroy(gameObject, Death_Timer);
    }
}
