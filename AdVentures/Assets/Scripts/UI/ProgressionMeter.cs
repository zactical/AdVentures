using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMeter : MonoBehaviour
{
    private Image progressionImage;
    private Animator animator;

    private float fillAmount;
    private bool isAnimating;
    private bool updatedThisFrame;

    private void Awake()
    {
        progressionImage = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void UpdateAmount(float amount)
    {
        fillAmount = amount;

        if(fillAmount >= 1)
        {
            fillAmount = 0;
            isAnimating = true;
            animator.SetTrigger("FullMeter");
        }

        updatedThisFrame = true;
    }

    public void FullMeterFinished()
    {
        isAnimating = false;
        UpdateVisual();
        GameManager.Instance.SpawnLoot();
    }

    private void UpdateVisual()
    {
        if (isAnimating)
            return;

        progressionImage.fillAmount = fillAmount;
    }

    private void LateUpdate()
    {
        if(updatedThisFrame)
            UpdateVisual();
    }
}
