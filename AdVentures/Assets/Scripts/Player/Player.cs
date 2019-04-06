using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerProjectileController))]
[RequireComponent(typeof(PlayerImmunity))]
[RequireComponent(typeof(PlayerLoot))]
public class Player : MonoBehaviour, ITakeDamage, IGrabLoot
{   

    // other player systems
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;
    private PlayerProjectileController playerProjectileController;
    private PlayerImmunity playerImmunity;
    private PlayerLoot playerLoot;
    private Animator animator;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
        playerProjectileController = GetComponent<PlayerProjectileController>();
        playerImmunity = GetComponent<PlayerImmunity>();
        playerLoot = GetComponent<PlayerLoot>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerShoot.ToggleShootingEnabled(false);
        animator.SetTrigger("PlayerSpawn");
    }

    public void AddWeapon(WeaponUpgradeTypeEnum type, float? expireInSeconds = null)
    {
        playerShoot.AddWeapon(type, expireInSeconds);
    }

    public void AddGenericUpgrade(GenericUpgradeEnum upgrade, float? expireInSeconds = null)
    {
        if (upgrade == GenericUpgradeEnum.Projectile)
            playerProjectileController.AddProjectile(expireInSeconds);
        else if (upgrade == GenericUpgradeEnum.Immunity)
            playerImmunity.SetImmunity(expireInSeconds ?? 0);
    }

    public void TakeDamage(int amount)
    {
        if (playerImmunity.IsImmune)
            return;

        animator.SetTrigger("KillPlayer");
        playerShoot.ToggleShootingEnabled(false);
        playerMovement.CanMove = false;
        GameManager.Instance.GameOver();
    }

    public void PickUpLoot(Loot loot)
    {
        if(loot.LootType.progressionAmount > 0)
            playerLoot.AddProgression(loot.LootType.progressionAmount);    
        else if (loot.LootType.weaponUpgrade != WeaponUpgradeTypeEnum.None)
        {
            AddWeapon(loot.LootType.weaponUpgrade, loot.LootType.upgradeDuration);
            UIManager.Instance.SetAlert(loot.LootType);
        }
        else if (loot.LootType.genericUpgrade != GenericUpgradeEnum.None)
        {
            AddGenericUpgrade(loot.LootType.genericUpgrade, loot.LootType.upgradeDuration);
            UIManager.Instance.SetAlert(loot.LootType);
        }
    }

    // called via Animation
    public void OnPlayerSpawnAnimationOver()
    {
        playerShoot.ToggleShootingEnabled(true);
    }

    public List<WeaponUpgradeTypeEnum> GetCurrentWeapons()
    {
        return playerShoot.GetCurrentWeapons();
    }

    public void ToggleShootingEnabled(bool enabled)
    {
        playerShoot.ToggleShootingEnabled(enabled);
    }

    public void ToggleMovingEnabled(bool enabled)
    {
        playerMovement.ToggleCanMove(enabled);
    }
}
