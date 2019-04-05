using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : PooledMonoBehaviour, ITakeDamage
{
    [SerializeField]
    private EnemyTypeEnum enemyType;

    // private member data     
    private int enemyHealth;
    private LootType lootOnKill;
    private bool canShoot;
    [SerializeField]
    private Loot itemToSpawn;

    // other enemy systems
    private EnemyGroup group;
    private EnemyAnimation enemyAnimation;
    private List<IDeathEvent> deathEvents;
    private IEnemyGraphicUpdater enemyGraphics;
    private IEnemyAttack enemyAttack;
    private IEnemyMove enemyMove;
    

    // public accessors
    public int StartingColumn { get; private set; }
    public EnemyTypeEnum EnemyType => enemyType;

    private void Awake()
    {
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();
        deathEvents = GetComponentsInChildren<IDeathEvent>().ToList();
        enemyGraphics = GetComponent<IEnemyGraphicUpdater>();
        enemyAttack = GetComponent<IEnemyAttack>();
        enemyMove = GetComponent<IEnemyMove>();        
        StartingColumn = Mathf.RoundToInt(transform.position.x);
    }

    private void OnEnable()
    {
        enemyHealth = enemyGraphics == null ? 1 : enemyGraphics.GraphicLevels();
        enemyGraphics?.UpdateGraphics(enemyHealth);
        canShoot = true;
    }

    public void TakeDamage(int amount)
    {
        enemyHealth--;

        if (enemyHealth <= 0)
        {
            Kill();
        }
        else
            enemyGraphics?.UpdateGraphics(enemyHealth);
    }       

    public void SetGroup(EnemyGroup group)
    {
        this.group = group;
    }

    public void FlyAway(float delay)
    {
        canShoot = false;
        enemyAnimation.FlyAway(delay);
    }

    public void SetLootToAward(LootType loot)
    {
        lootOnKill = loot;
    }

    public void Kill(bool awardLoot = true)
    {

        if (awardLoot)
        {
            GameManager.Instance.AddToScore(100);

            if (lootOnKill != null)
            {
                // RaiseDeathEvents();         
                var item = Instantiate(itemToSpawn, transform.position, Quaternion.identity);
                item.SetLootType(lootOnKill);
                lootOnKill = null;
            }
        }

        group.ReportEnemyDeath(this);
        ReturnToPool();
    }


    #region Enemy Systems Passthrough

    public void Move(MovementXDirectionEnum xDirection, MovementYDirectionEnum yDirection)
    {
        enemyMove?.Move(xDirection, yDirection);
    }

    public void Shoot()
    {
        if(canShoot)
            enemyAttack?.Attack();
    }

    #endregion

    private void RaiseDeathEvents()
    {
        foreach (var deathEvent in deathEvents)
        {
            deathEvent.Raise();
        }
    }
}