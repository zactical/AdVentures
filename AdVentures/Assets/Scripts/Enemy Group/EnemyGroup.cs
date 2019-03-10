using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(EnemyGroupMovement))]
[RequireComponent(typeof(EnemyGroupAttack))]
public class EnemyGroup : MonoBehaviour
{
    public int SpawnNumber { get; private set; }    

    private List<Enemy> enemies = new List<Enemy>();
    private List<Enemy> deadEnemies = new List<Enemy>();
    private int currentActiveEnemies;
        
    // other enemy systems
    private EnemyManager enemyManager;
    private EnemyGroupMovement movement;
    private EnemyGroupAttack attack;
    private EnemyGroupRow[] enemyRows;

    private void OnValidate()
    {
        var number = this.name.Replace("EnemyGroup_", "");
        SpawnNumber = int.Parse(number);      
    }

    private void Awake()
    {
        movement = GetComponent<EnemyGroupMovement>();
        attack = GetComponent<EnemyGroupAttack>();        
    }    

    public void Initialize(EnemyManager manager, int startingX, int startingY)
    {
        enemyManager = manager;
        movement.Initialize(startingX, startingY);

        enemyRows = GetComponentsInChildren<EnemyGroupRow>();

        foreach (var row in enemyRows)
            enemies.AddRange(row.Initialize(enemyManager));

        attack.LateInitialize(enemies, this);
        movement.LateInitialize(enemies);

        ResetGroup();
    }

    public void ResetGroup()
    {
        var allRows = GetComponentsInChildren<EnemyGroupRow>();
        currentActiveEnemies = allRows.Sum(x => x.EnemyCount);
    }

    public void UpdateGroupMovement()
    {
        movement.UpdateGroupMovement();
    }

    public void ReportEnemyDeath(Enemy enemy)
    {
        if (deadEnemies.Contains(enemy))
            return;

        attack.ReportEnemyDeath(enemy);

        deadEnemies.Add(enemy);
        currentActiveEnemies--;

        if (currentActiveEnemies <= 0)
        {
            enemyManager.RemoveActiveGroup(this);
            enemyManager.SpawnNextGroup();
            Destroy(gameObject);
        }
    }    
}