using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlertPanel : PooledMonoBehaviour
{
    [SerializeField]
    private Image staffImage;
    [SerializeField]
    private TextMeshProUGUI alertText;

    private List<StaffScriptable> staffMembers = new List<StaffScriptable>();

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        staffMembers = ScriptableObjectUtils.GetAllInstances<StaffScriptable>().ToList();
    }

    public void TriggerAlert(LootType loot)
    {
        //var randomStaff = staffMembers[Random.Range(0, staffMembers.Count)];

        staffImage.sprite = loot.staff.Image;
        alertText.text = string.Format("{0} trained you on Ventures!", loot.staff.Name);

        animator.SetTrigger("Activate");
    }

    public void OnAnimationFinished()
    {
        ReturnToPool();
    }
}
