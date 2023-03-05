using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonterxController : MonoBehaviour
{
    public float Speed = 1f;
    public Transform target;
    public int Health = 100;
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
}
