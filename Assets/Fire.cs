using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    public GameObject skillPrefab;
    public Button button;
    public float showDuration = 5f;
    public float cooldownDuration = 20f;

    private bool isCooldown = false;

    private void Start()
    {
        skillPrefab.SetActive(false);
    }

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

}
