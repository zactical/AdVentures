using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : UpgradeBase
{
    public WeaponUpgradeTypeEnum Weapon;   

    public WeaponType(WeaponUpgradeTypeEnum weapon, float? expiresInSeconds)
    {
        Weapon = weapon;
        ExpiresInSeconds = expiresInSeconds;
    }
}
