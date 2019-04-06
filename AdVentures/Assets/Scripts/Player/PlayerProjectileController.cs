using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerShoot))]
public class PlayerProjectileController : MonoBehaviour
{
    [SerializeField]
    private List<Projectile> projectiles;
    private PlayerShoot playerShoot;

    private int currentProjectileLevel = 1;
    private float currentLevelDuration;
    private float maxDurationPerLevel = 5f;

    private void Awake()
    {
        playerShoot = GetComponent<PlayerShoot>();        
    }

    private void Start()
    {
        if (projectiles == null || projectiles.Count == 0)
            Debug.LogError("Must specify some projectiles.");

        DoUpdateProjectile();
    }

    private void Update()
    {       
        //if (currentLevelDuration > 0)
        //    currentLevelDuration -= Time.deltaTime;
        //
        //if (currentProjectileLevel > (int)ProjectileUpgradeTypeEnum.Standard && currentLevelDuration <= 0)
        //    DecreaseLevel();
    }

    public void AddProjectile(float? expireInSeconds = null)
    {
        if(expireInSeconds.HasValue)
            maxDurationPerLevel = expireInSeconds.Value;

        IncreaseLevel();
    }

    private void DoUpdateProjectile()
    {
        playerShoot.SetProjectile(projectiles.FirstOrDefault(x => (int)x.projectileType == currentProjectileLevel));
    }

    private void IncreaseLevel()
    {
        currentLevelDuration = maxDurationPerLevel;
        currentProjectileLevel = Mathf.Min(Enum.GetValues(typeof(ProjectileUpgradeTypeEnum)).Length - 1, currentProjectileLevel + 1);
        DoUpdateProjectile();
    }

    private void DecreaseLevel()
    {
        currentLevelDuration = maxDurationPerLevel;
        currentProjectileLevel = Mathf.Max(1, currentProjectileLevel - 1);
        DoUpdateProjectile();
    }
}
