using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngledShot : ShotBase
{
    public override void Fire(Projectile projectile, float force, IShotLocations shotLocations)
    {
        var shot = projectile.Get<Projectile>(shotLocations.Left().position, Quaternion.identity);
        shot.RigidBody.AddForce(Vector2.up * force);
    }
}
