using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPanel : MonoBehaviour
{    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Toggle();
    }

    public void Toggle()
    {
        animator.SetTrigger("Fade");
    }

    public void OnAnimationOver()
    {
        gameObject.SetActive(false);
    }
}
