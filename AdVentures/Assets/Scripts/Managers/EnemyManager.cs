﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour {

   // [SerializeField]
    private List<EnemyGroup> enemyGroupPrefabs;
    private List<Enemy> enemyPrefabs;
    [SerializeField]
    private Transform leftBound;
    [SerializeField]
    private Transform rightBound;
    [SerializeField]
    private float enemyMoveSpeed = 5f;
    [SerializeField]
    private int startingY = 4;

    private List<EnemyGroup> activeGroups = new List<EnemyGroup>();
    private float lastMoved;
    private float moveThreshold = 10f;
    private int leftBoundX;
    private int rightBoundX;

    private int spawnCounter;

    private void Awake()
    {
        leftBoundX = Mathf.RoundToInt(leftBound.transform.position.x);
        rightBoundX = Mathf.RoundToInt(rightBound.transform.position.x);

        enemyGroupPrefabs = Resources.LoadAll<EnemyGroup>("").ToList();
        enemyPrefabs = Resources.LoadAll<Enemy>("").ToList();
    }

    void Update() {
        lastMoved += Time.deltaTime;

        if (lastMoved * enemyMoveSpeed >= moveThreshold)
        {
            lastMoved = 0f;

            foreach (var group in activeGroups)
            {
                group.UpdateGroupMovement();
            }
        }
    }

    private void SpawnEnemies(EnemyGroup group)
    {
        var newGroup = Instantiate(group, new Vector3(0, startingY + group.OffsetStartingYPosition, 0), Quaternion.identity);
        newGroup.Initialize(this, 0, startingY);
        activeGroups.Add(newGroup);
    }

    public void RemoveActiveGroup(EnemyGroup group)
    {
        if (activeGroups.Contains(group))
            activeGroups.Remove(group);
    }


    public void SpawnNextGroup()
    {
        spawnCounter++;
        var group = enemyGroupPrefabs.FirstOrDefault(x => x.SpawnNumber == spawnCounter);

        if (group == null)
            Debug.LogError("No more groups to create");
        else
            SpawnEnemies(group);   
    }

    public Enemy GetEnemyByType(EnemyTypeEnum enemyType)
    {
        var enemy = enemyPrefabs.FirstOrDefault(x => x.EnemyType == enemyType);

        if (enemy == null)
            Debug.LogError($"No enemy of type: {enemyType} was found in the enemy manager.");

        return enemy;
    }
}
