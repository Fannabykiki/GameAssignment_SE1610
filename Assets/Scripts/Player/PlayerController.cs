using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D rb;
    private float left_right;
    private float up_down;
    private bool isfacingRight = true;
    private Animator ani;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()

    {
        left_right = Input.GetAxis("Horizontal");
        up_down = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(left_right * speed, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, up_down * speed);
        flip();

        ani.SetFloat("move", Mathf.Abs(left_right));
        ani.SetFloat("move", Mathf.Abs(up_down));

    }

    void flip()
    {
        if(isfacingRight && left_right <0 || !isfacingRight && left_right > 0)
        {
            isfacingRight = !isfacingRight;
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

    }
}
