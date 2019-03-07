using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootType", menuName = "Scriptables/LootType")]
public class LootType : ScriptableObject
{
    public Sprite Icon;
    public int Points;

    public WeaponUpgradeTypeEnum weaponUpgrade;
    public ProjectileUpgradeTypeEnum projectileUpgrade;
    public GenericUpgradeEnum genericUpgrade;
    public float upgradeDuration = 15f;
}
