using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    GameObject Player; //Player Gameobject

    void Start()
    {
        Player = gameObject.transform.parent.gameObject; //Access Parent's code
    }

    private void OnCollisionEnter2D(Collision2D collision) //When entering collision
    {
        if (collision.collider.tag == "Ground") //When hitting ground
        {
            Player.GetComponent<Player_Movement>()._IsGrounded = true; //Player is grounded
        }
    }

    private void OnCollisionExit2D(Collision2D collision) //When exiting collision
    {
        if (collision.collider.tag == "Ground") //When exiting ground
        {
            Player.GetComponent<Player_Movement>()._IsGrounded = false; //Player is not grounded
        }
    }
}
