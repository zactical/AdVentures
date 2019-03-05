using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public int SpawnNumber { get; private set; }

    private List<Enemy> enemies = new List<Enemy>();
    private EnemyManager enemyManager;

    private int groupCenterX;
    private int groupCenterY;
    private MovementXDirectionEnum directionX = MovementXDirectionEnum.Left;
    private MovementYDirectionEnum directionY = MovementYDirectionEnum.None;

    private int minMovementX = -3;
    private int maxMovementX = 3;

    private int currentActiveEnemies;

    private void OnValidate()
    {
        var number = this.name.Replace("EnemyGroup_", "");
        SpawnNumber = int.Parse(number);
    }

    private void Start()
    {
        enemies = GetComponentsInChildren<Enemy>().ToList();
        ResetGroup();

        foreach (var enemy in enemies)
        {
            enemy.SetGroup(this);
        }
    }

    public void Initialize(EnemyManager manager, int startingX, int startingY)
    {
        enemyManager = manager;
        groupCenterX = startingX;
        groupCenterY = startingY;
    }

    public void ResetGroup()
    {
        var allRows = GetComponentsInChildren<EnemyRow>();
        currentActiveEnemies = allRows.Sum(x => x.EnemyCount);
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

    public void ReportEnemyDeath(Enemy enemy)
    {
        currentActiveEnemies--;

        if (currentActiveEnemies <= 0)
            enemyManager.SpawnNextGroup();
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