using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; set; }

  //  [SerializeField]
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

    // Start is called before the first frame update
    void Start()
    {
        RegisterForEvents();
        possibleLoot = ScriptableObjectUtils.GetAllInstances<LootType>().ToList();
        enemyManager = FindObjectOfType<EnemyManager>();
        player = FindObjectOfType<Player>();

        if (enemyManager == null)
            Debug.LogError("Could not find Enemy Manager");
        else
            enemyManager.SpawnNextGroup();
    }

    private void RegisterForEvents()
    {
        Events.Register<Loot>(GameEventsEnum.LootPickedUp, OnLootPickedUp);
    }

    private void OnLootPickedUp(Loot item)
    {
        if(item.LootType.weaponUpgrade != WeaponUpgradeTypeEnum.None)
        {
            player.AddWeapon(item.LootType.weaponUpgrade, item.LootType.upgradeDuration);
        }
        else if(item.LootType.genericUpgrade != GenericUpgradeEnum.None)
        {
            player.AddGenericUpgrade(item.LootType.genericUpgrade, item.LootType.upgradeDuration);
        }
    }

    public LootType GetNextLoot()
    {
       // var currentWeaponTypes = player.CurrentWeapons.Select(x => x.Weapon);
       // var lootPlayerDoesNotHave = possibleLoot.Where(x => currentWeaponTypes.Contains(x.weaponUpgrade) == false).ToList();
        var randomLootIndex = Random.Range(0, possibleLoot.Count);
        return possibleLoot[randomLootIndex];
    }
}
