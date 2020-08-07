using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject EnemySprite; //Sprite of Enemy
    public GameObject DeathEffect; //DeathEffect of Enemy

    public int Health = 100; //Health of Enemy
    public float Speed = 1f; //Speed of Enemy
    public float Idle = 1f; //Idle period of Enemy
    public int ContactDamage = 20; //Damage dealth when Player contacts Enemy
    public double PlayerInvincibilityTimer = 5.0; //Gives Player invincibility after damage

    public Transform GroundCheck; //Checks Ground 
    public Vector2 GroundChecksize; //GroundCheck Size

    public Transform FrontCheck; //Checks Front
    public Vector2 FrontChecksize; //FrontCheck Size

    public Transform TargetCheck; //Checks availability of Target
    public Vector2 TargetChecksize; //TargetCheck Size

    public Transform RangeCheck; //Checks if Target is in Range of Enemy
    public Vector2 RangeChecksize; //RangeCheck Size

    private bool _CanMove; //Enemy can move
    private bool _IsFlipped; //Enemy is flipped
    private bool _PlayerSpotted = false; //Player has been spotted

    private double _Tracker; //Tracks how much time has passed
    private bool _PlayerFollow = false; //Stops following Player

    private void Start()
    {
        _CanMove = true; //Enemy can move
        _IsFlipped = false; //Enemy starts not flipped
        _Tracker = PlayerInvincibilityTimer; //Player Invincibility is tracked
    }

    void Update()
    {
        DetectPlayer(); //Dectects if Player is in range
        EnemyMovement(); //Controls Enemy Movement
        DetectFloor(); //Detects if it is grounded or hitting a wall
        PlayerInvincibility(); //Control Player Invincibility
    }

    private void DetectPlayer()
    {
        if (EnemySprite.GetComponent<EnemyWeapon>().FireCheckBool()) //If Player is in Enemy range
        {
            _CanMove = false; //Enemy stops moving
            _PlayerSpotted = true; //Player has been spotted
        }
        else if (!FrontCheckBool() && GroundCheckBool() && !_PlayerSpotted)
        //When Enemy is not next to a wall, is grounded, and Player has not been spotted
        {
            _CanMove = true; //Enemy moves
        }

        if (TargetCheckBool() && _PlayerSpotted) //When Enemy has seen Player
        {
            if (!EnemySprite.GetComponent<EnemyWeapon>().FireCheckBool()) //When Enemy is not facing Player
            {
                _IsFlipped = !_IsFlipped; //Flips Enemy

                if (_IsFlipped)
                {
                    transform.Rotate(0f, 180f, 0f);
                    Speed *= -1;
                }
                else if (!_IsFlipped)
                {
                    transform.Rotate(0f, 180f, 0f);
                    Speed *= -1;
                }
                //Enemy Rotation
            }
        }

        if (!RangeCheckBool() && !EnemySprite.GetComponent<EnemyWeapon>().FireCheckBool()) //When Player is not in range of Enemy
        {
            _PlayerSpotted = false; //Player is no longer spotted
        }
    }

    private void EnemyMovement()
    {
        if (_CanMove) //If Enemy can move
        {
            transform.position += Vector3.left * Speed * Time.deltaTime; //Enemy moves to the left
        }
    }

    private void DetectFloor() //Controls ground and wall detection
    {
        if (!GroundCheckBool() && _CanMove) //When there is no Ground and can move
        {
            _CanMove = false; //Enemy can't move

            if (!_PlayerSpotted) //If Player is not spotted
            {
                StartCoroutine(ChangeDirection()); //Changes Direction
            }
        }
        else if (!GroundCheckBool() && !_PlayerSpotted && _PlayerFollow)
        //When there is no ground, player is not spotted, and is following player
        {
            StartCoroutine(ChangeDirection()); //Changes Direction
            _PlayerFollow = false; //Enemy is no longer following player
        }

        if (FrontCheckBool() && _CanMove) //When Enemy reaches wall and can move
        {
            _CanMove = false; //Enemy cannot move
            StartCoroutine(ChangeDirection()); //Enemy changes Direction
        }
    }

    private void PlayerInvincibility()
    {
        PlayerInvincibilityTimer += Time.deltaTime; //Updating timer of Invincibility
    }

    private IEnumerator ChangeDirection()
    {
        if (!_PlayerSpotted) //When Player is not spotted
        {
            yield return new WaitForSeconds(Idle); //Enemy Waits
        }

        if (!_PlayerSpotted) //When Player is not spotted after waiting
        {
            _IsFlipped = !_IsFlipped; //Enemy changes Direction

            if (_IsFlipped)
            {
                transform.Rotate(0f, 180f, 0f);
                Speed *= -1;
            }
            else if (!_IsFlipped)
            {
                transform.Rotate(0f, 180f, 0f);
                Speed *= -1;
            }
            //Enemy Rotation

            _CanMove = true; //Enemy can move
        }

        if (_PlayerSpotted) //When Player has been spotted
        {
            _PlayerFollow = true; //Enemy follows player
        }

    }

    public void TakeDamage(int Damage) //Enemy takes damage
    {
        Health -= Damage; //Health is reduced

        if (Health <= 0) //When Health reaches zero
        {
            Die(); //Enemy is erased
        }
    }

    void Die() //Enemy erasure
    {
        Instantiate(DeathEffect, transform.position, transform.rotation); //DeathEffect animation
        Destroy(gameObject); //Enemy is erased
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) //When Enemy contacts Player
    {
        Player_Movement player = hitInfo.GetComponent<Player_Movement>(); //Gets Code

        if (PlayerInvincibilityTimer > _Tracker && hitInfo.tag == "Player") //When Player hits Enemy
        {
            PlayerInvincibilityTimer = 0; //Starts invincibility timer
            player.TakeDamage(ContactDamage); //Players takes damage
        }
    }

    private void OnTriggerStay2D(Collider2D hitInfo) //When Enemy is continually contacting Player
    {
        Player_Movement player = hitInfo.GetComponent<Player_Movement>(); //Gets Code

        if (PlayerInvincibilityTimer > _Tracker && hitInfo.tag == "Player") //When Player hits Enemy
        {
            PlayerInvincibilityTimer = 0; //Starts invincibility timer
            player.TakeDamage(ContactDamage); //Players takes damage
        }

    }

    private void OnDrawGizmosSelected() //Draws boxes to show trigger
    {
        Gizmos.DrawWireCube(GroundCheck.position, GroundChecksize);
        Gizmos.DrawWireCube(FrontCheck.position, FrontChecksize);
        Gizmos.DrawWireCube(TargetCheck.position, TargetChecksize);
        Gizmos.DrawWireCube(RangeCheck.position, RangeChecksize);
    }

    private bool GroundCheckBool() //Ground Check
    {
        return Physics2D.OverlapBox(GroundCheck.position, GroundChecksize, 0, LayerMask.GetMask("Default"));
    }

    private bool FrontCheckBool() //Wall Check
    {
        return Physics2D.OverlapBox(FrontCheck.position, FrontChecksize, 0, LayerMask.GetMask("Default"));
    }

    private bool TargetCheckBool() //When Player is facing Enemy
    {
        return Physics2D.OverlapBox(TargetCheck.position, TargetChecksize, 0, LayerMask.GetMask("Player"));
    }

    private bool RangeCheckBool() //When Player is in range of Enemy
    {
        return Physics2D.OverlapBox(RangeCheck.position, RangeChecksize, 0, LayerMask.GetMask("Player"));
    }
}