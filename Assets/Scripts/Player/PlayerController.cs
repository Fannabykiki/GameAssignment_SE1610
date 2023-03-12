using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator ani;
    public GameObject swordHitbox;
    Collider2D swordCollider;
    public Transform characterTransform;
    public Transform swordTransform;
    private int playerHealth = 100;
<<<<<<< Updated upstream
    public float speed;
    
    //move
=======
    public float speed = 5f;
    GameObject player;
    //moveW
>>>>>>> Stashed changes
    private float left_right;
    private float up_down;
    private bool isfacingRight = true;
    
    public bool isAttacking = false;
    //attack
    public float attackRange;
    public int attackDamage;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
<<<<<<< Updated upstream
        
=======
        skillPrefab.SetActive(false);
        skillPrefab2.SetActive(false);
        swordCollider = swordHitbox.GetComponent<Collider2D>();
        characterTransform = player.transform;
        swordTransform = transform;
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()

    {
        if (characterTransform.localScale.x > 0)
        {
            // nếu nhân vật quay sang phải, xoay gameobject kiếm về bên phải
            swordTransform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // nếu nhân vật quay sang trái, xoay gameobject kiếm về bên trái
            swordTransform.localScale = new Vector3(-1, 1, 1);
        }
        //move
        if (!isAttacking)
        {
            left_right = Input.GetAxis("Horizontal");
            up_down = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(left_right * speed, rb.velocity.y);
            rb.velocity = new Vector2(rb.velocity.x, up_down * speed);
            
        }
        //animation
        flip();
        ani.SetFloat("move", Mathf.Abs(left_right));
        AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
        {

            ani.SetTrigger("Idle");
        }

        //attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ani.SetTrigger("attack");
            isAttacking = true;
            rb.velocity = Vector2.zero;
            //Attack();
        }
        

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

    public void EndAttack()
    {
        isAttacking = false;
    }
    //void Attack()
    //{
    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

    //    foreach (Collider2D enemy in hitEnemies)
    //    {
    //        enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
    //    }
    //}
}
