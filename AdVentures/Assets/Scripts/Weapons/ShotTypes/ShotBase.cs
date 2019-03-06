using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShotBase : IWeapon
{
    public virtual void Fire(Projectile projectile, float force, IShotLocations shotLocations)
    {
        
    }
}
