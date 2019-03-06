using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType
{
    public WeaponUpgradeTypeEnum Weapon;
    public float? ExpiresInSeconds;

    public WeaponType(WeaponUpgradeTypeEnum weapon, float? expiresInSeconds)
    {
        Weapon = weapon;
        ExpiresInSeconds = expiresInSeconds;
    }

    public bool IsExpired()
    {
        if (ExpiresInSeconds.HasValue == false)
            return false;

        return ExpiresInSeconds.Value > 0;        
    }

    public void UpdateTime(float amount)
    {
        if (ExpiresInSeconds.HasValue == false)
            return;

        ExpiresInSeconds -= amount;
    }
}
