using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monterx : MonoBehaviour, IDamageable
{
   
    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;
    bool isAlive = true;
    public float Health
    {
        set
        {
            if (value < _health)
            {
                animator.SetTrigger("hit");
            }
            _health = value;

            if (_health <= 0)
            {
                animator.SetBool("isAlive", false);
                Targetable = false;
            }
        }
        get
        {
            return _health;
        }
    }

    public bool Targetable
    {
        get { return _targetable; }
        set
        {
            _targetable = value;
            rb.simulated = value;
            physicsCollider.enabled = value;
        }
    }

    public float _health = 100;

    public bool _targetable = true;
    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }
   
    //effects from skills
    public void TakeDamage(float damagePlayer)
    {
        Health -= damagePlayer;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        rb.AddForce(knockback);
        Debug.Log("Force" + knockback);
    }

    public void OnHit(float damage)
    {
        Health -= damage;
    }

    public void MakeUntargetable()
    {
        rb.simulated = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.tag == "Tower")
        {
           
            Vector2 direction = -(collision.transform.position - transform.position); //tính hướng đẩy
            direction = direction.normalized * 10; //đưa hướng về 1
            rb.AddForce(direction * 500f); //đẩy quái với lực 500
        }
        if (collision.transform.tag == "Player")

        {
            
            if (rb != null)
            {
                Vector2 direction = -(collision.transform.position - transform.position); //tính hướng đẩy
                direction = direction.normalized * 5; //đưa hướng về 1
                
            }
        }
    }

    public void OnObjectDestroyed()
    {
        Destroy(gameObject);
    }
}


