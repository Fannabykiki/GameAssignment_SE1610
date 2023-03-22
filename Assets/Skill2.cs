using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill2 : MonoBehaviour
{
    public Button skillButton; // Button to activate skill
    public GameObject skill;   // Skill to activate
    public float showDuration = 5f;
    public float cooldownDuration = 10f;
    public float radius = 5f; // Phạm vi xóa
    private bool isCooldown = false;

    void Start()
    {
        // Add listener for button click event
        skill.SetActive(false);
        skillButton.onClick.AddListener(ActivateSkill);
    }

    void ActivateSkill()
    {
        // Activate skill
        if (!isCooldown)
        {
            skill.SetActive(true);
            Invoke("HideSkill", showDuration);
            isCooldown = true;
            skillButton.interactable = false;
            Invoke("EndCooldown", cooldownDuration);
        }
    }
    private void HideSkill()
    {
        skill.SetActive(false);
    }
}
