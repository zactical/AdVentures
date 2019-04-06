using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; set; }


    [SerializeField]
    private Transform lootTopBound;
    [SerializeField]
    private Transform lootBottomBound;
    [SerializeField]
    private Transform lootLeftBound;
    [SerializeField]
    private Transform lootRightBound;



    private List<LootType> possibleLoot = new List<LootType>();

    public Loot lootPrefab;

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
        Pool.GetPool(lootPrefab).WarmPool();
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
        SceneManager.LoadScene(1);
        Pool.ClearPools();
        //    Time.timeScale = 1;
    }

    public void SpawnLoot()
    {
        var availableLoot = possibleLoot.Where(x => x.weaponUpgrade != WeaponUpgradeTypeEnum.None ||
            (x.genericUpgrade != GenericUpgradeEnum.None && x.genericUpgrade != GenericUpgradeEnum.Immunity)
        ).ToList();
        var playersWeapons = player.GetCurrentWeapons();
        var lootNotYetPickedUp = availableLoot.Where(x => playersWeapons.Contains(x.weaponUpgrade) == false).ToList();

        var randomWeapon = lootNotYetPickedUp[Random.Range(0, lootNotYetPickedUp.Count)];

        var item = lootPrefab.Get<Loot>(GetRandomLootPosition(), Quaternion.identity);
        item.SetLootType(randomWeapon);
        item.SetGravityScale(0);
    }

    private void SetupLoot()
    {
        possibleLoot = ScriptableObjectUtils.GetAllInstances<LootType>().Where(x => x.IsActive == true && x.progressionAmount == 0).ToList();
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
        player.ToggleMovingEnabled(false);
        player.ToggleShootingEnabled(false);
        UIManager.Instance.ShowGameOverScreen(score, isAllLevelsFinished ? "You Win!" : "Game Over");
    }


    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSeconds(2);
        enemyManager.SpawnNextGroup();
    }

    private Vector3 GetRandomLootPosition()
    {
        var newPosition = new Vector3(0, 0, 0);
        newPosition.x = Random.Range(lootLeftBound.position.x, lootRightBound.position.x);
        newPosition.y = Random.Range(lootTopBound.position.y, lootBottomBound.position.y);

        return newPosition;
    }
}
