using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator ani;


    private int playerHealth = 100;
    public float speed = 5f;
    
    //move
    private float left_right;
    private float up_down;
    private bool isfacingRight = true;
    //attack
    public float attackRange;
    public int attackDamage;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    //speedUp
    public float speedBoost = 2f;
    private float speedUpTime ;
    public float SpeedUpTime = 1f;
    bool speedOnce = false;
    ////skill1
    public GameObject skillPrefab;
    public Button button;
    public float showDuration = 5f;
    public float cooldownDuration = 20f;

    private bool isCooldown = false;


    //Skill1
    public void ShowSkill()
    {
        if (!isCooldown)
        {
            skillPrefab.SetActive(true);
            Invoke("HideSkill", showDuration);
            isCooldown = true;
            button.interactable = false;
            Invoke("EndCooldown", cooldownDuration);
        }
    }

    private void HideSkill()
    {
        skillPrefab.SetActive(false);
    }

    private void EndCooldown()
    {
        isCooldown = false;
        button.interactable = true;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        skillPrefab.SetActive(false);

    }

    // Update is called once per frame
    void Update()

    {
        //move
        left_right = Input.GetAxis("Horizontal");
        up_down = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(left_right * speed, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, up_down * speed);

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
            //Attack();
        }

        //SpeedUp
        if(Input.GetKeyDown(KeyCode.Z) && speedUpTime <= 0)
        {
            speed += speedBoost;
            speedUpTime = SpeedUpTime;
            speedOnce = true;

        }
        if(speedUpTime<= 0 && speedOnce == true)
        {
            speed -= speedBoost;
            speedOnce = false;
        }
        else
        {
            speedUpTime -= Time.deltaTime;
        }
        //Skill
        if (Input.GetKeyDown(KeyCode.X))
        {
            ShowSkill();
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
    //void Attack()
    //{
    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

    //    foreach (Collider2D enemy in hitEnemies)
    //    {
    //        enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
    //    }
    //}
}
