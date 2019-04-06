using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootOnDeath : MonoBehaviour, IDeathEvent
{
    [SerializeField]
    private Loot itemToSpawn;
    [SerializeField]
    [Range(0, 100)]
    private int dropChancePercent = 50;
    
    public void Raise()
    {
        // if (itemToSpawn == null)
        //    return;

        if (Random.Range(1, 100) <= dropChancePercent)
        {
            var item = itemToSpawn.Get<Loot>(transform.position, Quaternion.identity);
            item.SetLootType(GameManager.Instance.GetNextLoot());
        }
    }
}
