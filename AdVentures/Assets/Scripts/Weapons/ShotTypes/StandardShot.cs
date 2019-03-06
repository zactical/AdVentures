using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardShot : ShotBase
{
    public override void Fire(Projectile projectile, float force, IShotLocations shotLocations)
    {
        var shot = projectile.Get<Projectile>(shotLocations.Normal().position, Quaternion.identity);
        //  shot.Launch(force);
        shot.RigidBody.AddForce(Vector2.up * force); 
    }
}
