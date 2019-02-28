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
    private List<IWeapon> currentWeapons;

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

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        currentWeapons = new List<IWeapon>();
        currentWeapons.Add(new StandardShot());
        currentWeapons.Add(new AngledShot());
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
                weapon.Fire(projectile, shotForce, this);
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
}
