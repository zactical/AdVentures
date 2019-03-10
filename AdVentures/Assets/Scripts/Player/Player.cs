using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerProjectileController))]
[RequireComponent(typeof(PlayerImmunity))]
public class Player : MonoBehaviour, ITakeDamage, IGrabLoot
{
    

    // other player systems
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;
    private PlayerProjectileController playerProjectileController;
    private PlayerImmunity playerImmunity;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
        playerProjectileController = GetComponent<PlayerProjectileController>();
        playerImmunity = GetComponent<PlayerImmunity>();
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

        playerShoot.ToggleShootingEnabled(false);
        StartCoroutine(KillAfterSeconds(2));
    }

    public void PickUpLoot(Loot loot)
    {
        if (loot.LootType.weaponUpgrade != WeaponUpgradeTypeEnum.None)
        {
            AddWeapon(loot.LootType.weaponUpgrade, loot.LootType.upgradeDuration);
        }
        else if (loot.LootType.genericUpgrade != GenericUpgradeEnum.None)
        {
            AddGenericUpgrade(loot.LootType.genericUpgrade, loot.LootType.upgradeDuration);
        }
    }

    private IEnumerator KillAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameManager.Instance.PlayerDied();
    }

    
}
