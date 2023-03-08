﻿using System.Collections;
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
    public float knockbackForce;
    private Vector3 initialPosition;
    void Start()
    {
        currentHealth = Health;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Di chuyển quái vật về trụ thành
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * Speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Nếu quái vật va chạm với trụ thành, đẩy nó ra khỏi trụ thành
        if (collision.gameObject.CompareTag("Tower"))
        {
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            knockback(direction);
        }
    }
    void knockback(Vector2 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Nếu va chạm với nhân vật
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                _ = player.attackDamage; // Trừ máu của nhân vật
                Destroy(gameObject); // Biến mất khỏi màn hình
            }
        }
    }


    private void Die()
    {
        // Khi quái vật chết, xóa nó khỏi scene
        Destroy(gameObject);
    }
}