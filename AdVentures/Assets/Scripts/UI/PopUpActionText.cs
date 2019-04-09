using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpActionText : PooledMonoBehaviour
{
    private TextMeshProUGUI popupText;
    private Animator animator;

    private void Awake()
    {
        popupText = GetComponentInChildren<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }

    public void SetScoreText(int amount)
    {
        popupText.text = string.Format("+{0}", amount);
        animator.SetTrigger("Fade");
    }

    public void OnFadeFinished()
    {
        ReturnToPool();
    }
}
