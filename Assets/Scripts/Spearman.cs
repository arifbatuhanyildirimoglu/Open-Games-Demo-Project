using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spearman : MonoBehaviour
{
    private Animator animator;
    private bool isHappy = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isHappy)
        {
            isHappy = false;
            animator.SetTrigger("isHappy");
        }
    }

    public bool IsHappy
    {
        get => isHappy;
        set => isHappy = value;
    }
}
