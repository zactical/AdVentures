using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Loot : MonoBehaviour
{
   // [SerializeField]
    private LootType lootType;
    private SpriteRenderer renderer;

    public LootType LootType => lootType;

    private void Awake()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetLootType(LootType lootType)
    {
        this.lootType = lootType;
        renderer.sprite = this.lootType.Icon;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Events.Raise(GameEventsEnum.LootPickedUp, this);
        Destroy(gameObject);
    }
}
