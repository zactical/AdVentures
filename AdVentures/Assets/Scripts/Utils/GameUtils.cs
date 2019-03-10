using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class GameUtils
{
    public static T[] GetAllInstances<T>() where T : UnityEngine.Object
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;

    }

    public static Enemy GetEnemyPrefabByType(EnemyTypeEnum enemyType)
    {
        var enemyPrefabs = Resources.LoadAll<Enemy>("").ToList();
        var enemy = enemyPrefabs.FirstOrDefault(x => x.EnemyType == enemyType);

        if (enemy == null)
            Debug.LogError($"No enemy of type: {enemyType} was found in the enemy manager.");

        return enemy;
    }
}
