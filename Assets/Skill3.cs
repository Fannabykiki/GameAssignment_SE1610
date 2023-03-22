using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill3 : MonoBehaviour
{
    public Button skillButton; // Button to activate skill
    public float showDuration = 5f;
    public float cooldownDuration = 10f;
    public float radius = 5f; // Phạm vi xóa
    private bool isCooldown = false;
    public float speed = 5f;
    public float speedBoost = 10f;
    private float speedUpTime;
    public float SpeedUpTime = 1f;
    bool speedOnce = false;
    void Start()
    {
        // Add listener for button click event
        skillButton.onClick.AddListener(ActivateSkill);
    }

    void ActivateSkill()
    {
        // Activate skill
        //skill.SetActive(true);
        if (!isCooldown) { 
            if (speedUpTime <= 0)
            {
                speed += speedBoost;
                speedUpTime = SpeedUpTime;
                speedOnce = true;
                isCooldown = true;

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

        }
    }
}
