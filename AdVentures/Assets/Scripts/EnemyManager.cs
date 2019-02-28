using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private List<EnemyGroup> enemyGroupPrefabs;
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

    private void Awake()
    {
        leftBoundX = Mathf.RoundToInt(leftBound.transform.position.x);
        rightBoundX = Mathf.RoundToInt(rightBound.transform.position.x);
    }

    // Use this for initialization
    void Start () {
        SpawnEnemies(enemyGroupPrefabs[0]);
	}
	
	// Update is called once per frame
	void Update () {
        lastMoved += Time.deltaTime;

        if(lastMoved * enemyMoveSpeed >= moveThreshold)
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
        var newGroup = Instantiate(group, new Vector3(0, startingY, 0), Quaternion.identity);
        newGroup.Initialize(0, startingY);
        activeGroups.Add(newGroup);
    }
}
