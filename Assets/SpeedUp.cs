using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float speed = 10f;
    public float acceleration = 1f;
    public float deceleration = 1f;
    public float maxSpeed = 20f;
    public float cooldownTime = 1f;

    private float timeRemaining;
    private bool isCooldown;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isCooldown)
        {
            speed += acceleration;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
            isCooldown = true;
            timeRemaining = cooldownTime;
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            speed -= deceleration;
            if (speed < 0f)
            {
                speed = 0f;
            }
        }

        if (isCooldown)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0f)
            {
                isCooldown = false;
            }
        }

        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
