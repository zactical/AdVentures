using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField]
    private Projectile projectile;
    [SerializeField]
    private float shotForce = 200f;

    public void Attack()
    {
        var shot = projectile.Get<Projectile>(transform.position, Quaternion.identity);
        shot.SetLaunchDirection(Vector2.down);
        shot.Launch(shotForce);
    }
}
