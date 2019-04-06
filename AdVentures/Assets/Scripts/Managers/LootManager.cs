using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootManager : MonoBehaviour
{

    [SerializeField]
    private Transform lootStartingPosition;

    [Header("Loot Bounds")]
    [SerializeField]
    private Transform lootTopBound;
    [SerializeField]
    private Transform lootBottomBound;
    [SerializeField]
    private Transform lootLeftBound;
    [SerializeField]
    private Transform lootRightBound;

    private List<LootType> possibleLoot = new List<LootType>();

    public Loot lootPrefab;

    private void Start()
    {
        Pool.GetPool(lootPrefab).WarmPool();
        SetupLoot();
    }

    public void SpawnLoot()
    {
        if (possibleLoot.Count <= 0)
            return;

        //  var randomLoot = possibleLoot[Random.Range(0, possibleLoot.Count)];

        var randomLoot = possibleLoot[0];
        possibleLoot.RemoveAt(0);

        DoSpawnLoot(randomLoot);
    }    

    private void DoSpawnLoot(LootType loot)
    {
        var targetLootPosition = GetRandomLootPosition();
        var item = lootPrefab.Get<Loot>(lootStartingPosition.position, Quaternion.identity);
        item.SetLootType(loot);
        item.SetGravityScale(0);
        item.MoveToTarget(targetLootPosition);
    }

    private Vector3 GetRandomLootPosition()
    {
        var newPosition = new Vector3(0, 0, 0);
        newPosition.x = Random.Range(lootLeftBound.position.x, lootRightBound.position.x);
        newPosition.y = Random.Range(lootTopBound.position.y, lootBottomBound.position.y);

        return newPosition;
    }

    private void SetupLoot()
    {
        var discoveredLoot = ScriptableObjectUtils.GetAllInstances<LootType>().Where(x => x.IsActive == true && x.progressionAmount == 0).ToList();
        var staffMembers = ScriptableObjectUtils.GetAllInstances<StaffScriptable>().ToList();

        foreach (var loot in discoveredLoot)
        {
            var randomUnusedStaff = staffMembers[Random.Range(0, staffMembers.Count)];
            loot.staff = randomUnusedStaff;

            staffMembers.Remove(randomUnusedStaff);
        }

        var possibleWeapons = discoveredLoot.Where(x => x.weaponUpgrade != WeaponUpgradeTypeEnum.None).ToList();
        var possibleNonWeapons = discoveredLoot.Where(x => (x.genericUpgrade != GenericUpgradeEnum.None && x.genericUpgrade != GenericUpgradeEnum.Immunity)).ToList();


        // randomize order of weapons first in possible loot
        for (int i = 0; i < possibleWeapons.Count; i++)
            possibleLoot.Add(possibleWeapons[Random.Range(0, possibleWeapons.Count)]);

        possibleLoot.AddRange(possibleNonWeapons);    
    }
}
