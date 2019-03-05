using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; set; }

    private EnemyManager enemyManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();

        if (enemyManager == null)
            Debug.LogError("Could not find Enemy Manager");
        else
            enemyManager.SpawnNextGroup();
    }
}
