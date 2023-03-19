using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonteryController : MonoBehaviour
{
    public float Speed = 2f;
    public Transform target;
    public int Health = 3;
    public int damage = 10;
    private int currentHealth;

    private Rigidbody2D rb;
 
    private PlayerController playerController;
    private MonteryController monteryController;
    void Start()
    {
        currentHealth = Health;
        rb = GetComponent<Rigidbody2D>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        // Di chuyển quái vật về trụ thành
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * Speed;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (monteryController != null)
        {
            damage = (int)monteryController.damage;
        }
        if (collision.transform.tag == "Tower")
        {
            Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
            Vector2 direction = -(collision.transform.position - transform.position); //tính hướng đẩy
            direction = direction.normalized * 10; //đưa hướng về 1
            rb.AddForce(direction * 500f); //đẩy quái với lực 500
        }
        if (collision.transform.tag == "Player")
            
            {
                    Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = -(collision.transform.position - transform.position); //tính hướng đẩy
                direction = direction.normalized * 5; //đưa hướng về 1
                rb.AddForce(direction * 300f); //đẩy quái với lực 300
            }
                }
            
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
            }        }
    }

    private void Die()
    {
        GetComponent<LootBag>().InstantiatateLoot(transform.position);
        // Khi quái vật chết, xóa nó khỏi scene
        Destroy(gameObject);
    }
    void OnHit(int damage)
    {        currentHealth -= damage;
    }
}