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

    [SerializeField]
    private int maxMovesToMake = 10;
    [SerializeField]
    private int offsetStartingYPosition = 0;
    public int OffsetStartingYPosition => offsetStartingYPosition;

    [SerializeField]
    private List<EnemyGroupLootDetail> progressionDrops;


    private int currentMovesTaken = 0;
    private bool isCurrentlyExiting = false;

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
        RandomizeGroupLoot();
    }

    public void ResetGroup()
    {
        var allRows = GetComponentsInChildren<EnemyGroupRow>();
        currentActiveEnemies = allRows.Sum(x => x.EnemyCount);
    }

    public void UpdateGroupMovement()
    {
        if(currentMovesTaken >= maxMovesToMake)
        {
            if(isCurrentlyExiting == false)
                ForceGroupExit();

            return;
        }

        movement.UpdateGroupMovement();
        currentMovesTaken++;
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
    
    public void ForceGroupExit()
    {
        if (isCurrentlyExiting)
            return;

        isCurrentlyExiting = true;
        float delay = 0f;
        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                enemy.FlyAway(delay);
                delay = Mathf.Min(delay + .2f, 1.5f);
            }
        }
    }

    private void RandomizeGroupLoot()
    {
        var enemiesWithoutLoot = enemies.ToList();

        for (int i = 0; i < progressionDrops.Count; i++)
        {
            for (int k = 0; k < progressionDrops[i].count; k++)
            {
                var randomEnemy = enemiesWithoutLoot[Random.Range(0, enemiesWithoutLoot.Count)];
                randomEnemy.SetLootToAward(progressionDrops[i].loot);
                enemiesWithoutLoot.Remove(randomEnemy);
            }
        }
    }
}