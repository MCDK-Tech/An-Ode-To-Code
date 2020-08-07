using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public GameObject DeathEffect; //Gameobject DeathEffect
    private Animator Animator; //Animator

    public int Health; //Player's Health
    public int Speed; //Player's Speed
    public float JumpForce; //Player's Jump Force
    public int ExtraJumps; //Jumps
    public double AtkAnimationDuration = 0.5; //Duration of Animation

    public bool _IsGrounded = false; //Player is grounded
    public bool _HasGun = false; //Player has gun

    private bool _FacingRight = true; //Sprite is facing Right
    private int _ExtraJumpsCounter; //Jump counter
    private double _AnimationTracker; //Animation Tracker
    private bool _CanShoot = false; //Player is able to shoot

    void Start()
    {
        _ExtraJumpsCounter = ExtraJumps; //Extra jumps saved
        _AnimationTracker = AtkAnimationDuration; //Attack duration tracked

        Animator = GetComponent<Animator>(); //Animator set up
        Animator.SetBool("IdleWITHGUN", false); //Gun holding sprite is false
    }

    void Update()
    {
        Attack(); //Controls attacking
        Move(); //Controls movement
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && _IsGrounded && AtkAnimationDuration > _AnimationTracker && _CanShoot)
        //When player can attack
        {
            Animator.SetTrigger("Attack"); //Animation starts
            AtkAnimationDuration = 0; //Animation duration resets to zero
        }

        if (Input.GetKeyDown(KeyCode.F) && !_CanShoot && _HasGun)
        //When player is not holding a gun and F is pressed
        {
            Animator.SetBool("IdleWITHGUN", true); //Sprite change
            _CanShoot = true; //Player can shoot
        }
        else if (Input.GetKeyDown(KeyCode.F) && _CanShoot)
        //When player is holding a gun and F is pressed
        { 
            Animator.SetBool("IdleWITHGUN", false); //Sprite changes to original
            _CanShoot = false; //Player cannot shoot
        }
    }

    public void Move()
    {
        AtkAnimationDuration += Time.deltaTime; //Updates time

        if (AtkAnimationDuration > _AnimationTracker) //When there is no animation
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f); //Player can move
            transform.position += movement * Time.deltaTime * Speed;
        }

        if (!_FacingRight && Input.GetAxis("Horizontal") > 0 && AtkAnimationDuration > _AnimationTracker)
        //When facing right and player moves left
        {
            _FacingRight = !_FacingRight;
            transform.Rotate(0f, 180f, 0f); //Player sprite flips
        }
        else if (_FacingRight && Input.GetAxis("Horizontal") < 0 && AtkAnimationDuration > _AnimationTracker)
        //When facing left and player moves right
        {
            _FacingRight = !_FacingRight;
            transform.Rotate(0f, 180f, 0f); //Player sprite flips
        }

        Jump(); //Controls jumping
    }

    void Jump()
    {
        if (_IsGrounded) //When player is grounded
        {
            _ExtraJumpsCounter = ExtraJumps; //Jumps reset
        }

        if (_ExtraJumpsCounter > 0) //When player can Jump
        {
            if (Input.GetButtonDown("Jump") && AtkAnimationDuration > _AnimationTracker) //When jump button is pressed
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                //Player jumps
                _ExtraJumpsCounter--; //Number of jumps left decreases by 1
            }
        }
        else if (_ExtraJumpsCounter == 0 && _IsGrounded) //When there are no extra jumps and player is grounded
        {
            if (Input.GetButtonDown("Jump") && AtkAnimationDuration > _AnimationTracker) //When jump button is pressed
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                //Player jumps
            }
        }
    }

    public bool CanShootBool() //Checks if player can shoot for other scipts
    {
        return _IsGrounded && _CanShoot ? true : false;
    }

    public void TakeDamage(int damage) //Player damage calculation
    {
        Health -= damage; //Health taken off by damage

        if (Health <= 0) //When health reaches zero
        {
            Die(); //Player dies
        }
    }

    void Die() //Destroys game object of player
    {
        Instantiate(DeathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
