using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Run);
    }

    void Run()
    {
        animator.SetBool("isRunning", true);
    }
}
