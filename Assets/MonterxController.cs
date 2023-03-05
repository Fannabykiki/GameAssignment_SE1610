using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonterxController : MonoBehaviour
{
    public float Speed = 1f;
    public Transform target;
    public int Health = 100;
    private int currentHealth;
    public int damage = 0;
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
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-5f, 5f); // Bật quái vật ra khỏi trụ thành
        }
        
    }

    private void Die()
    {
        // Khi quái vật chết, xóa nó khỏi scene
        Destroy(gameObject);
    }
}

