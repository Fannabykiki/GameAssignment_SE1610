using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public int swordDamage = 1;
    public float knockbackForce = 500f;
    public Collider2D swordCollider;
   
    // Start is called before the first frame update
    void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("not set");
        }
        //swordCollider.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        IDamageable damageableObject = col.gameObject.GetComponent<IDamageable>();
        if(damageableObject != null)
        { //knockback
            Vector3 parenPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction = (Vector2)(  col.gameObject.transform.position - parenPosition).normalized;
            Vector2 knockback = direction * knockbackForce;
            //col.gameObject.SendMessage("OnHit", swordDamage, knockback);
            damageableObject.OnHit(swordDamage, knockback);
        }
        else
        {
            Debug.LogWarning("col does not implement IDamageable");
        }
        
    }
    
    
}
