using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour,IDataPersistence
{
    // Start is called before the first frame update
    private bool isAlive = true;
    private Rigidbody2D rb;
    private Animator ani;
    
    Collider2D swordCollider;
    public Transform characterTransform;
    public Transform swordTransform;
    private int playerMaxHealth = 100;
    private int currentHealth;
    public float speed = 5f;
    GameObject player;
    public TextMeshProUGUI scoreText;
    public Slider healthSlider;
    public GameObject gameOverPanel;
   
    //die
    private SpriteRenderer spriteRenderer;
    public float blinkTime = 0.1f;
    public float waitTime = 5f;
    private float elapsedTime = 0f; // Thời gian đã trôi qua sau khi hết máu
    private bool canMove = true;
    private bool canAttack = true;
    //moveW
    public Joystick movementJoystick;
    private float left_right;
    private float up_down;
    private bool isfacingRight = true;

    public bool isAttacking = false;
    //attack
    public float attackRange;
    public int attackDamage;
    public Transform attackPoint;
    public LayerMask enemyLayers;
   
    //speedUp
    public float speedBoost = 10f;
    private float speedUpTime;
    public float SpeedUpTime = 1f;
    bool speedOnce = false;
    ////skill1
    public GameObject skillPrefab;
    public Button button;
    public float showDuration = 5f;
    public float cooldownDuration = 20f;
    public float radius = 5f; // Phạm vi xóa

    private bool isCooldown = false;
    //Skill2
    public GameObject skillPrefab2;
    public Button button2;
    public float showDuration2 = 5f;
    public float cooldownDuration2 = 10f;
    
    private bool isCooldown2 = false;

    public float damage = 10f;
    public float knockback = 500f;
    //Skill1
    public float knockbackDuration = 1f;

    public void ClearEnemiesInRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Destroy(collider.gameObject);
            }
        }
    }
    public void ShowSkill1()
    {
        if (!isCooldown)
        {
            skillPrefab.SetActive(true);
            Invoke("HideSkill", showDuration);
            ClearEnemiesInRadius();
            isCooldown = true;
            button.interactable = false;
            Invoke("EndCooldown", cooldownDuration);
        }
    }
    //Skill2
    public void ShowSkill2()
    {
        if (!isCooldown2)
        {
            skillPrefab2.SetActive(true);
            Invoke("HideSkill", showDuration2);
            isCooldown2 = true;
            button2.interactable = false;
            Invoke("EndCooldown", cooldownDuration2);
        }
    }

    //Skill

    private void HideSkill()
    {
        skillPrefab.SetActive(false);
        skillPrefab2.SetActive(false);
    }
    private void EndCooldown()
    {
        isCooldown = false;
        button.interactable = true;
        isCooldown2 = false;
        button2.interactable = true;
    }

    void Start()
    {
        Debug.Log(ScoreScript.scoreValue);
        ScoreScript.scoreValue = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = playerMaxHealth;
        healthSlider.maxValue = playerMaxHealth;
        healthSlider.value = playerMaxHealth;
        gameOverPanel.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        skillPrefab.SetActive(false);
        skillPrefab2.SetActive(false);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        //characterTransform = player.transform;
        swordTransform = transform;


    }
    void FixedUpdate()
    {
        if (!isAttacking && canMove)
        {
            if (movementJoystick.joystickVec.y != 0)
            {
                rb.velocity = new Vector2(movementJoystick.joystickVec.x * speed, movementJoystick.joystickVec.y * speed);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        flip();
        ani.SetFloat("move", Mathf.Abs(movementJoystick.joystickVec.x));
        
        AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
        {

            ani.SetTrigger("Idle");
        }

    }
    // Update is called once per frame
    public void Attack()
    {
        if(canAttack)
        {
            ani.SetTrigger("attack");
            isAttacking = true;
            rb.velocity = Vector2.zero;
        }
    }
    void Update()

    {

        

        //if (!isAttacking && canMove)
        //{
        //    left_right = Input.GetAxis("Horizontal");
        //    up_down = Input.GetAxis("Vertical");
        //    rb.velocity = new Vector2(left_right * speed, rb.velocity.y);
        //    rb.velocity = new Vector2(rb.velocity.x, up_down * speed);

        //}
        ////animation
        //flip();
        //ani.SetFloat("move", Mathf.Abs(left_right));
        //AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);
        //if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1f)
        //{

        //    ani.SetTrigger("Idle");
        //}

        //attack
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            ani.SetTrigger("attack");
            isAttacking = true;
            rb.velocity = Vector2.zero;

            //Attack();


        }


        //SpeedUp
        if (Input.GetKeyDown(KeyCode.Z) && speedUpTime <= 0)
        {
            speed += speedBoost;
            speedUpTime = SpeedUpTime;
            speedOnce = true;

        }
        if (speedUpTime <= 0 && speedOnce == true)
        {
            speed -= speedBoost;
            speedOnce = false;
        }
        else
        {
            speedUpTime -= Time.deltaTime;
        }
        //Skill1
        if (Input.GetKeyDown(KeyCode.X))
        {
            ShowSkill1();
        }
        //Skill2
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject skill = Instantiate(skillPrefab2);
            skill.transform.SetParent(transform);
            skill.transform.localPosition = Vector3.zero;
            skill.transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);
            ShowSkill2();
        }

        //die
        if (currentHealth <= 0)
        {
            elapsedTime += Time.deltaTime; // Tính thời gian đã trôi qua


            if (elapsedTime >= waitTime)
            {
                currentHealth += 100;
                healthSlider.value = currentHealth;
                canMove = true;
                canAttack = true;
                StopCoroutine(Blink());
                spriteRenderer.enabled = true;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                GetComponent<Collider2D>().enabled = true;
                // Thiết lập lại thời gian đếm về 0
                elapsedTime = 0f;
                
            }
            else
            {
                canMove = false;
                canAttack = false;
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                StartCoroutine(Blink());


            }

        }
    }

    IEnumerator Blink()
    {

        spriteRenderer.enabled = !spriteRenderer.enabled;
        yield return new WaitForSeconds(blinkTime);

    }
    void flip()
    {

        if (isfacingRight && movementJoystick.joystickVec.x < 0 || !isfacingRight && movementJoystick.joystickVec.x > 0)
        {
            isfacingRight = !isfacingRight;

            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MonsterZ"))
        {
            if(!isAttacking)
            {
                Debug.Log(currentHealth);
                currentHealth -= 15;
                healthSlider.value = currentHealth;
                if (currentHealth <= 0)
                {
                    canMove = false;
                    canAttack = false;
                    spriteRenderer.enabled = false;
                }
            }
            
           

        }
        else if (collision.gameObject.CompareTag("MonsterY"))
        {
            if (!isAttacking)
            {
                currentHealth -= 10;
                healthSlider.value = currentHealth;
                if (currentHealth <= 0)
                {
                    canMove = false;
                    canAttack = false;
                    spriteRenderer.enabled = false;
                }
            }

        }
        if (collision.gameObject.CompareTag("Monterx"))  
        {
            StartCoroutine(Knockback());
        }
    }
    private IEnumerator Knockback()  //Monterx va chạm nhân vật
    {
        // Đóng băng player
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        // Chờ 1 giây
        yield return new WaitForSeconds(knockbackDuration);

        // Mở khóa player
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void ShowGameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        scoreText.text = "Your Score: " + ScoreScript.scoreValue.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ICollectible collectible = collision.GetComponent<ICollectible>();
        //if (collectible != null)
        //{
        //    currentHealth += 10;
        //    currentHealth = Mathf.Clamp(currentHealth, 0, playerMaxHealth);
        //    collectible.Collect();
        //}

        //effects on monsters
        //if (collision.CompareTag("Enemy"))
        //{
        //    Tính toán sát thương
        //    MonterxController enemy = collision.GetComponent<MonterxController>();
        //    enemy.TakeDamage(damage);
        //    MonteryController enemy1 = collision.GetComponent<MonteryController>();
        //    enemy1.TakeDamage(damage);
        //    MonterzController enemy2 = collision.GetComponent<MonterzController>();
        //    enemy2.TakeDamage(damage);

        //    Áp dụng knockback
        //   Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();
        //    Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
        //    enemyRb.AddForce(knockbackDirection * knockback);

        //    Vector2 knockbackDirection1 = (enemy1.transform.position - transform.position).normalized;
        //    enemyRb.AddForce(knockbackDirection1 * knockback);

        //    Vector2 knockbackDirection2 = (enemy2.transform.position - transform.position).normalized;
        //    enemyRb.AddForce(knockbackDirection2 * knockback);
        //}
    }

    public void EndAttack()
    {
        isAttacking = false;
    }
    public void LoadData(GameData gameData)
    {
        this.currentHealth = gameData.currentHealth;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.currentHealth = this.currentHealth;
    }
    //void Attack()
    //{
    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


}
