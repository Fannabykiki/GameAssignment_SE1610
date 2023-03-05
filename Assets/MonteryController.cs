using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonteryController : MonoBehaviour
{
    public float Speed = 2f;
    public Transform target;
    public int Health = 300;
    public int damage = 10;
    private int currentHealth;

    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = Health;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * Speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce((transform.position - collision.transform.position) * 500f);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Khi quái vật chết, xóa nó khỏi scene
        Destroy(gameObject);
    }
}