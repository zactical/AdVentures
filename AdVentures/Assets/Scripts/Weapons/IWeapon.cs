using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Fire(Projectile projectile, float force, IShotLocations shotLocations);
}

public interface IShotLocations
{
    Transform Left();
    Transform Normal();
    Transform Right();
}