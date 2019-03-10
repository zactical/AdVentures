using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMove
{
    void Move(MovementXDirectionEnum xDirection, MovementYDirectionEnum yDirection);
}
