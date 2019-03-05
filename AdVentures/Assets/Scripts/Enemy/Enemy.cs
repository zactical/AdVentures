using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PooledMonoBehaviour, ITakeDamage {

    private EnemyGroup group;

    public void TakeDamage(int amount)
    {
        throw new System.NotImplementedException();
    }

    public void Move(MovementXDirectionEnum xDirection, MovementYDirectionEnum yDirection)
    {
        if (yDirection != MovementYDirectionEnum.None)
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
        else if (xDirection == MovementXDirectionEnum.Left)
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
        else if (xDirection == MovementXDirectionEnum.Right)
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
    }

    public void SetGroup(EnemyGroup group)
    {
        this.group = group;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        group.ReportEnemyDeath(this);
        ReturnToPool();        
    }
}
