using System.Collections;
using System.Collections.Generic;

public interface IWeapon
{
    void Fire(Projectile projectile, float force, IShotLocations shotLocations);
}
