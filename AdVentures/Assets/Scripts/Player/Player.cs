using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerProjectileController))]
public class Player : MonoBehaviour
{   
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;
    private PlayerProjectileController playerProjectileController;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
        playerProjectileController = GetComponent<PlayerProjectileController>();
    }
    

    public void AddWeapon(WeaponUpgradeTypeEnum type, float? expireInSeconds = null)
    {
        playerShoot.AddWeapon(type, expireInSeconds);
    }

    public void AddGenericUpgrade(GenericUpgradeEnum upgrade, float? expireInSeconds = null)
    {
        if(upgrade == GenericUpgradeEnum.Projectile)
            playerProjectileController.AddProjectile(expireInSeconds);
    }

    
}
