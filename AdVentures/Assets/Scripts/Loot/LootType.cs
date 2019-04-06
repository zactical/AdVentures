using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LootType", menuName = "Scriptables/LootType")]
public class LootType : ScriptableObject
{
    public bool IsActive = true;
    public Sprite Icon;
    public int Points;

    public WeaponUpgradeTypeEnum weaponUpgrade;
    public ProjectileUpgradeTypeEnum projectileUpgrade;
    public GenericUpgradeEnum genericUpgrade;
    public float upgradeDuration = 15f;
    public int progressionAmount;

    public StaffScriptable staff;

    private const string INACTIVE_TEXT = "(Inactive)";

    public void OnValidate()
    {
        if (IsActive == false && name.Contains(INACTIVE_TEXT) == false)
        {
            ChangeName((name + INACTIVE_TEXT).Trim());
        }
        else if (IsActive && name.Contains(INACTIVE_TEXT))
            ChangeName(name.Replace(INACTIVE_TEXT, "").Trim());
    }

    private void ChangeName(string newName)
    {
        var path = AssetDatabase.GetAssetPath(GetInstanceID());        
        AssetDatabase.RenameAsset(path, newName);
        AssetDatabase.SaveAssets();
    }
}
