using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour, IShotLocations
{
    [SerializeField]
    private Projectile projectile;
    [SerializeField]
    float moveSpeed = 8f;
    [SerializeField]
    float shootSpeed = 15f;
 //   [SerializeField]
    private List<WeaponType> currentWeapons;

    [Header("Shot Locations")]
    [SerializeField]
    private Transform left;
    [SerializeField]
    private Transform normal;
    [SerializeField]
    private Transform right;


    private PlayerInput playerInput;
    private float lastShot;
    private float shotThreshold = 10f;
    private float shotForce = 300f;

    public List<WeaponType> CurrentWeapons => currentWeapons;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        currentWeapons = new List<WeaponType>();
        currentWeapons.Add(new WeaponType(WeaponUpgradeTypeEnum.Normal, null));
       // currentWeapons.Add(new AngledShot());
    }

    private void Update()
    {
        foreach (var item in currentWeapons)
        {
            item.UpdateTime(Time.deltaTime);
            if (item.ExpiresInSeconds.HasValue && item.ExpiresInSeconds <= 0)
                StartCoroutine(RemoveWeaponAtEndOfFrame(item));
        }
    }

    private void LateUpdate()
    {
        if (playerInput.isMovingLeft)
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        else if (playerInput.isMovingRight)
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);

        if(CanShoot())
        {
            foreach (var weapon in currentWeapons)
            {
                ShotFactory.Fire(weapon.Weapon, projectile, shotForce, this);
            }
        }
    }

    private bool CanShoot()
    {
        lastShot += Time.deltaTime * shootSpeed;

        if(lastShot > shotThreshold)
        {
            lastShot = 0f;
            return true;
        }

        return false;
    }

    public Transform Left()
    {
        return left;
    }

    public Transform Normal()
    {
        return normal;
    }

    public Transform Right()
    {
        return right;
    }

    public void AddWeapon(WeaponUpgradeTypeEnum type, float? expireInSeconds = null)
    {
        currentWeapons.Add(new WeaponType(type, expireInSeconds));
    }

    private IEnumerator RemoveWeaponAtEndOfFrame(WeaponType type)
    {
        yield return new WaitForEndOfFrame();

        currentWeapons.Remove(type);
    }
}
