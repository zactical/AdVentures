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

    // other enemy systems
    private EnemyGroup group;
    private List<IDeathEvent> deathEvents;
    private IEnemyGraphicUpdater enemyGraphics;
    private IEnemyAttack enemyAttack;
    private IEnemyMove enemyMove;

    // public accessors
    public int StartingColumn { get; private set; }
    public EnemyTypeEnum EnemyType => enemyType;

    private void Awake()
    {
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
    }

    public void TakeDamage(int amount)
    {
        enemyHealth--;

        if (enemyHealth <= 0)
        {
            RaiseDeathEvents();
            group.ReportEnemyDeath(this);
            ReturnToPool();
        }
        else
            enemyGraphics?.UpdateGraphics(enemyHealth);
    }       

    public void SetGroup(EnemyGroup group)
    {
        this.group = group;
    }



    #region Enemy Systems Passthrough

    public void Move(MovementXDirectionEnum xDirection, MovementYDirectionEnum yDirection)
    {
        enemyMove?.Move(xDirection, yDirection);
    }

    public void Shoot()
    {
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