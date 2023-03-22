using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class basicAttack : MonoBehaviour
{
    public Button skillButton; // Button to activate skill
    public GameObject skill;   // Skill to activate

    void Start()
    {
        // Add listener for button click event
        skillButton.onClick.AddListener(ActivateSkill);
    }

    void ActivateSkill()
    {
        // Activate skill
        skill.SetActive(true);

    }
}
