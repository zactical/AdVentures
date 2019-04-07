using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Loot : PooledMonoBehaviour
{
   // [SerializeField]
    private LootType lootType;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private MoveToTarget moveToTarget;
    private Vector3 defaultScale;

    public LootType LootType => lootType;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        moveToTarget = GetComponent<MoveToTarget>();
        defaultScale = transform.localScale;
    }

    public void SetLootType(LootType lootType)
    {
        this.lootType = lootType;

        if(this.lootType.staff?.Image != null)
            sr.sprite = this.lootType.staff.Image;
        else
            sr.sprite = this.lootType.Icon;       
    }

    public void SetGravityScale(float scale = 0.75f)
    {
        rb.gravityScale = scale;
    }

    public void SetTransformScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void MoveToTarget(Vector3 position, float duration = 3f)
    {
        moveToTarget.SetTarget(position);
        moveToTarget.timeToTarget = duration;
        moveToTarget.StartMoving();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        var lootGrabber = collider.gameObject.GetComponent<IGrabLoot>();

        if (lootGrabber != null)
            lootGrabber.PickUpLoot(this);

        transform.localScale = defaultScale;
        ReturnToPool();
    }
}
