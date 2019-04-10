using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System;

public class EnemyGroupRow : MonoBehaviour
{

    [SerializeField]
    private List<RowData> rowData;

    public int EnemyCount => rowData.Sum(x => x.Amount);
    
    public List<Enemy> Initialize(EnemyManager enemyManager, bool updateChildGraphicsOnSpawn = false)
    {
        var startingXposition = 0 - (EnemyCount / 2);

        int totalSpawned = 0;
        var spawnedEnemies = new List<Enemy>();

        foreach (var datum in rowData)
        {
            for (int i = 0; i < datum.Amount; i++)
            {
                var targetEndLocation = new Vector3(startingXposition + totalSpawned, transform.position.y, 0);
                var spawnLocation = new Vector3(targetEndLocation.x, targetEndLocation.y + 8, 0);
                var child = GetEnemy(enemyManager, GetEnemyPrefab(datum.EnemyType, enemyManager), spawnLocation);
                child.OnSpawn(targetEndLocation);

                if (updateChildGraphicsOnSpawn)
                    child.GetComponent<IEnemyGraphicUpdater>()?.UpdateGraphics();

                spawnedEnemies.Add(child);

                totalSpawned++;
            }
        }

        return spawnedEnemies;
    }

    // Manager can be null when testing in editor mode
    private Enemy GetEnemy(EnemyManager manager, Enemy enemy, Vector3 position)
    {
        if (manager == null)
            return Instantiate(enemy, position, Quaternion.identity, transform);
        else
            return enemy.Get<Enemy>(position, Quaternion.identity);
    }

    // Manager can be null when testing in editor mode
    private Enemy GetEnemyPrefab(EnemyTypeEnum enemyType, EnemyManager manager)
    {
        if (manager == null)
            return GameUtils.GetEnemyPrefabByType(enemyType);
        else
            return manager.GetEnemyByType(enemyType);
    }
}

[Serializable]
public class RowData
{
    public int Amount;
    public EnemyTypeEnum EnemyType;
}