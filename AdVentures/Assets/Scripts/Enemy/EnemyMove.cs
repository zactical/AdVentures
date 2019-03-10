using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour, IEnemyMove
{
    public void Move(MovementXDirectionEnum xDirection, MovementYDirectionEnum yDirection)
    {
        if (yDirection != MovementYDirectionEnum.None)
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
        else if (xDirection == MovementXDirectionEnum.Left)
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
        else if (xDirection == MovementXDirectionEnum.Right)
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
    }
}
