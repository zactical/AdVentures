using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; set; }

    private List<LootType> possibleLoot = new List<LootType>();

    private Player player;
    private EnemyManager enemyManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        possibleLoot = ScriptableObjectUtils.GetAllInstances<LootType>().Where(x => x.IsActive == true).ToList();
        enemyManager = FindObjectOfType<EnemyManager>();
        player = FindObjectOfType<Player>();

        if (enemyManager == null)
            Debug.LogError("Could not find Enemy Manager");
        else
            enemyManager.SpawnNextGroup();
    }

    public LootType GetNextLoot()
    {
       // var currentWeaponTypes = player.CurrentWeapons.Select(x => x.Weapon);
       // var lootPlayerDoesNotHave = possibleLoot.Where(x => currentWeaponTypes.Contains(x.weaponUpgrade) == false).ToList();
        var randomLootIndex = Random.Range(0, possibleLoot.Count);
        return possibleLoot[randomLootIndex];
    }

    public void PlayerDied()
    {
        SceneManager.LoadScene(0);
        Pool.ClearPools();
    }
}
