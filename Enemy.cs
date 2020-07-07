using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject DeathEffect;
    public Transform _groundCheck;
    public Vector2 _groundChecksize;
    public bool _canMove;
    public bool _isFlipped;
    public float Speed;


    void Start()
    {

    }

    void Update()
    {
        if (_canMove)
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
        }

        DetectFloor();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapBox(_groundCheck.position, _groundChecksize, 0, LayerMask.GetMask("Ground"));
    }

    private void DetectFloor()
    {
        if(!GroundCheck() && _canMove)
        {
            _canMove = false;
            StartCoroutine(ChangeTarget());
        }
    }

    private IEnumerator ChangeTarget()
    {
        _isFlipped = !_isFlipped;

        yield return new WaitForSeconds(5f);

        if(_isFlipped)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Speed *= -1;
        }
        else if(!_isFlipped)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Speed *= -1;
        }

        _canMove = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(_groundCheck.position, _groundChecksize);
    }
}