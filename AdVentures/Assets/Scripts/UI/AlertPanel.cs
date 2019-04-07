using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlertPanel : PooledMonoBehaviour
{
    [SerializeField]
    private SpriteRenderer IconSpriteRenderer;
    [SerializeField]
    private TextMeshProUGUI alertText;
    [SerializeField]
    private TextMeshProUGUI effectText;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerAlert(LootType loot)
    {
        IconSpriteRenderer.sprite = loot.staff.Image;
                
        alertText.text = string.Format("{0} {1}", loot.staff.Name, GameUtils.GetRandomTrainingMessage());
        effectText.text = loot.AdditionalPickupMessage;

        animator.SetTrigger("Activate");
    }

    public void OnAnimationFinished()
    {
        ReturnToPool();
    }
}
