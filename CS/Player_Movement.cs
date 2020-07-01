using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    
    public float speed;
    public float jumpForce;
    public float moveInputX;
    public float moveInputY;

    public bool facingRight = true;
    public int extraJumps;
    public int extraJumpsValue;
    public bool isGrounded = false;
    private Animator animator;
    public double animationTimer = 0.4;
    private double tracker;
    public bool hasGunAnimation = false;
    public bool recievedGun = false;

    void Start()
    {
        extraJumps = extraJumpsValue;
        animator = GetComponent<Animator>();
        tracker = animationTimer;
        animator.SetBool("IdleWITHGUN", false);
    }

    void Update()
    {
        //Extra Jumps
        if(isGrounded == true) 
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            Jump();
            extraJumps--;
        }
            else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true)
            {
                Jump();
            }
        //Extra Jumps End

        Attack();
        Shoot();
        


    }
    
    void FixedUpdate() {

        moveInputX = Input.GetAxis("Horizontal");
        moveInputY = Input.GetAxis("Vertical");

        Move();
        

        //Flip Character
        if (facingRight == false && moveInputX > 0 && animationTimer > tracker)
        {
            Flip();
        }
            else if(facingRight == true && moveInputX < 0 && animationTimer > tracker)
            {
                Flip();
            }

        
        //Flip Character End
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Jump() //Jump when Space is pressed
    {
        if (Input.GetButtonDown("Jump"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && isGrounded == true && animationTimer > tracker && hasGunAnimation)
        {
            animator.SetTrigger("Attack");
            animationTimer = 0;
        }
    }

    public bool canShoot()
    {
        if (isGrounded && hasGunAnimation)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Move()
    {
        animationTimer += Time.deltaTime;
        if (animationTimer > tracker)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * speed;
        }
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.F) && hasGunAnimation == false && recievedGun)
        {
            animator.SetBool("IdleWITHGUN", true);
            hasGunAnimation = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && hasGunAnimation)
        {
            animator.SetBool("IdleWITHGUN", false);
            hasGunAnimation = false;
        }
    }
}
