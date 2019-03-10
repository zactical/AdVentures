using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGroupAttack : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    private float attackDelay = 4f;

    [SerializeField]
    private int maxAllowableAttacksPerDelay = 1;

    private float lastShot;

    private Dictionary<int, List<Enemy>> enemiesByColumn = new Dictionary<int, List<Enemy>>();

    private void Start()
    {
        lastShot = attackDelay;
    }

    private void Update()
    {
        lastShot += Time.deltaTime;

        if (lastShot >= attackDelay)
        {
            lastShot = 0f;
            DoAttack();            
        }
    }

    private void DoAttack()
    {
        var activeColumns = enemiesByColumn.Where(x => x.Value.Count > 0).Select(x => x.Key).ToList();

        var perShotDelay = attackDelay / maxAllowableAttacksPerDelay;
        var pershotHalved = perShotDelay / 2;

        for (int i = 1; i <= maxAllowableAttacksPerDelay; i++)
        {
            if (activeColumns.Count == 0)
                break;

            var delay = (i * perShotDelay) + Random.Range(-pershotHalved, pershotHalved);
            var randomColumn = Random.Range(0, activeColumns.Count);

            StartCoroutine(AttackAfterTime(activeColumns[randomColumn], delay));
            activeColumns.RemoveAt(randomColumn);
        }
    }

    private IEnumerator AttackAfterTime(int columnIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        if(enemiesByColumn[columnIndex] != null && enemiesByColumn[columnIndex].Count > 0)
            enemiesByColumn[columnIndex].Last().Shoot();
    }

    public void LateInitialize(List<Enemy> enemies, EnemyGroup parentGroup)
    {
        foreach (var enemy in enemies)
        {
            enemy.SetGroup(parentGroup);

            var enemyColumn = Mathf.RoundToInt(enemy.StartingColumn);
            if (enemiesByColumn.ContainsKey(enemyColumn) == false)
                enemiesByColumn.Add(enemyColumn, new List<Enemy>());

            enemiesByColumn[enemyColumn].Add(enemy);
        }

        foreach (var column in enemiesByColumn.Keys.ToList())
        {
            enemiesByColumn[column] = enemiesByColumn[column].OrderByDescending(x => x.transform.position.y).ToList();
        }
    }

    public void ReportEnemyDeath(Enemy enemy)
    {
        if (enemiesByColumn[enemy.StartingColumn].Contains(enemy))
            enemiesByColumn[enemy.StartingColumn].Remove(enemy);
    }
}
