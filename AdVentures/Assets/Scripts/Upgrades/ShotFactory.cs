using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShotFactory
{
   public static void Fire(WeaponUpgradeTypeEnum type, Projectile projectile, float force, IShotLocations shotLocations)
    {
        switch (type)
        {
            case WeaponUpgradeTypeEnum.None:
                break;
            case WeaponUpgradeTypeEnum.Normal:
                NormalShot(projectile, force, shotLocations.Normal().position);
                break;
            case WeaponUpgradeTypeEnum.Left:
                NormalShot(projectile, force, shotLocations.Left().position);
                break;
            case WeaponUpgradeTypeEnum.Right:
                NormalShot(projectile, force, shotLocations.Right().position);
                break;
            case WeaponUpgradeTypeEnum.Double:
                break;
            case WeaponUpgradeTypeEnum.AngleLeft:
                AngleShot(projectile, force, shotLocations.Normal().position, -20);
                break;
            case WeaponUpgradeTypeEnum.AngleRight:
                AngleShot(projectile, force, shotLocations.Normal().position, 20);
                break;
            default:
                break;
        }
    }

    private static void NormalShot(Projectile projectile, float force, Vector3 position)
    {
        var shot = projectile.Get<Projectile>(position, Quaternion.identity);
        shot.RigidBody.AddForce(Vector2.up * force);
    }

    private static void AngleShot(Projectile projectile, float force, Vector3 position, float angle)
    {
        var shot = projectile.Get<Projectile>(position, Quaternion.identity);

        shot.transform.Rotate(new Vector3(0, 0, angle));

        shot.RigidBody.AddForce(shot.transform.up * force);
    }
}
