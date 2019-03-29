using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    [SerializeField]
    private AlertPanel alertPanel;
    [SerializeField]
    private RectTransform alertStartingLocation;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        var alertPool = Pool.GetPool(alertPanel, alertStartingLocation.parent.gameObject);
        alertPool.WarmPool();        
    }

    public void SetAlert(LootType loot)
    {
        var newAlert = alertPanel.Get<AlertPanel>(alertStartingLocation.transform.position, Quaternion.identity);
        newAlert.TriggerAlert(loot);
    }

}
