using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    private List<Enemy> enemies = new List<Enemy>();

    private int groupCenterX;
    private int groupCenterY;
    private MovementXDirectionEnum directionX = MovementXDirectionEnum.Left;
    private MovementYDirectionEnum directionY = MovementYDirectionEnum.None;

    private int minMovementX = -3;
    private int maxMovementX = 3;

    private void Awake()
    {
        enemies = GetComponentsInChildren<Enemy>().ToList();        
    }

    public void Initialize(int startingX, int startingY)
    {
        groupCenterX = startingX;
        groupCenterY = startingY;
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void UpdateGroupMovement()
    {
        UpdateDirection();

        foreach (var enemy in enemies)
        {
            enemy.Move(directionX, directionY);
        }
    }

    private void UpdateDirection()
    {
        if (groupCenterX <= minMovementX)
        {
            directionX = MovementXDirectionEnum.Right;
            directionY = MovementYDirectionEnum.Down;
            groupCenterX++;
            groupCenterY++;
        }
        else if (groupCenterX >= maxMovementX)
        {
            directionX = MovementXDirectionEnum.Left;
            directionY = MovementYDirectionEnum.Down;
            groupCenterX--;
            groupCenterY++;
        }
        else
        {
            directionY = MovementYDirectionEnum.None;
        }

        if(directionY == MovementYDirectionEnum.None)
        {
            if (directionX == MovementXDirectionEnum.Left)
                groupCenterX--;
            else
                groupCenterX++;
        }
    }
}

public enum MovementXDirectionEnum
{
    None,
    Left,
    Right
}

public enum MovementYDirectionEnum
{
    None,
    Up,
    Down
}