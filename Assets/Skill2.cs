using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill2 : MonoBehaviour
{
    public float damage = 10f;
    public float knockback = 500f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Tính toán sát thương
            Monterx enemy = collision.GetComponent<Monterx>();
            enemy.TakeDamage(damage);
            Montery enemy1 = collision.GetComponent<Montery>();
            enemy1.TakeDamage(damage);
            Monterz enemy2 = collision.GetComponent<Monterz>();
            enemy2.TakeDamage(damage);

            // Áp dụng knockback
            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();
            Vector2 knockbackDirection = (enemy1.transform.position - transform.position).normalized;
            enemyRb.AddForce(knockbackDirection * knockback);

            Vector2 knockbackDirection1 = (enemy1.transform.position - transform.position).normalized;
            enemyRb.AddForce(knockbackDirection1 * knockback);

            Vector2 knockbackDirection2 = (enemy2.transform.position - transform.position).normalized;
            enemyRb.AddForce(knockbackDirection2 * knockback);

            Destroy(gameObject);
        }
    }
}
