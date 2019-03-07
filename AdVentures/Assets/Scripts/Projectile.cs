using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PooledMonoBehaviour
{
    public ProjectileUpgradeTypeEnum projectileType;

    [SerializeField]
    private float defaultForceAmount = 100f;

    private new Rigidbody2D rigidbody;

    public Rigidbody2D RigidBody { get { return rigidbody; } }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Launch(float? force = null)
    {
        rigidbody.AddForce(Vector2.up * (force ?? defaultForceAmount));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ReturnToPool();
    }
}
