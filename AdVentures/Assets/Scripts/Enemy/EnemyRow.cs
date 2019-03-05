using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class EnemyRow : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    private int enemyCount;
    [SerializeField]
    private Enemy enemyPrefab;

    public int EnemyCount => enemyCount;

    private void OnValidate()
    {
        //if (enemyPrefab == null)
        //    return;
        //    var currentEnemies = GetComponentsInChildren<Enemy>();
        //
        //    foreach (var enemy in currentEnemies)
        //    {
        //        UnityEditor.EditorApplication.delayCall += () =>
        //        {
        //            DestroyImmediate(enemy.gameObject, true);
        //        };
        //    }
    }

    private void Awake()
    {
        var startingXposition = 0 - (enemyCount / 2);

        for (int i = 0; i < enemyCount; i++)
        {
             Instantiate(enemyPrefab, new Vector3(startingXposition + i, transform.position.y, 0), Quaternion.identity, transform.parent);
        }
    }
}
