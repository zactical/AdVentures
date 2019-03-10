using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImmunity : MonoBehaviour
{
    [SerializeField]
    private GameObject immunityVisual;

    private Animator animator;
    private float immunityTimeRemaining;
    private float fadingIndicatorThreshold = 2f;


    public bool IsImmune => immunityTimeRemaining > 0;

    private void Awake()
    {
        animator = immunityVisual.GetComponent<Animator>();
    }

    private void Update()
    {
        if (immunityTimeRemaining > 0)
        {
            immunityTimeRemaining -= Time.deltaTime;

            if (immunityTimeRemaining < fadingIndicatorThreshold)
                UpdateAnimatorVisual();
        }
        else if (immunityVisual.activeSelf == true)
            SetImmunity(immunityTimeRemaining);
    }

    public void SetImmunity(float duration)
    {
        immunityTimeRemaining = duration;
        immunityVisual.SetActive(immunityTimeRemaining > 0);
        UpdateAnimatorVisual();
    }

    private void UpdateAnimatorVisual()
    {
        animator.SetBool("ShieldFading", immunityTimeRemaining < fadingIndicatorThreshold);
    }
}
