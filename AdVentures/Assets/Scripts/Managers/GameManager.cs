using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; set; }

    private List<LootType> possibleLoot = new List<LootType>();

    private int score;
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
        enemyManager = FindObjectOfType<EnemyManager>();
        player = FindObjectOfType<Player>();
        SetupLoot();

        if (enemyManager == null)
            Debug.LogError("Could not find Enemy Manager");
        else
            StartCoroutine(StartGameAfterDelay());
    }

    public LootType GetNextLoot()
    {
       // var currentWeaponTypes = player.CurrentWeapons.Select(x => x.Weapon);
       // var lootPlayerDoesNotHave = possibleLoot.Where(x => currentWeaponTypes.Contains(x.weaponUpgrade) == false).ToList();
        var randomLootIndex = Random.Range(0, possibleLoot.Count);
        return possibleLoot[randomLootIndex];
    }

    public void AddToScore(int amount)
    {
        score += amount;
        UIManager.Instance.UpdateScore(score);
    }

    public void GameOver(bool isAllLevelsFinished = false)
    {
        enemyManager.DisperseAllEnemies();
        StartCoroutine(GameOverAfterSeconds(2, isAllLevelsFinished));
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
        Pool.ClearPools();
    //    Time.timeScale = 1;
    }

    private void SetupLoot()
    {
        possibleLoot = ScriptableObjectUtils.GetAllInstances<LootType>().Where(x => x.IsActive == true).ToList();
        var staffMembers = ScriptableObjectUtils.GetAllInstances<StaffScriptable>().ToList();

        foreach (var loot in possibleLoot)
        {
            var randomUnusedStaff = staffMembers[Random.Range(0, staffMembers.Count)];
            loot.staff = randomUnusedStaff;
            
            staffMembers.Remove(randomUnusedStaff);
        }
    }

    private IEnumerator GameOverAfterSeconds(float seconds, bool isAllLevelsFinished)
    {
        yield return new WaitForSeconds(seconds);
        //  Time.timeScale = 0;
        UIManager.Instance.ShowGameOverScreen(score, isAllLevelsFinished ? "You Win!" : "Game Over");
    }
    

    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSeconds(2);
        enemyManager.SpawnNextGroup();
    }
}
