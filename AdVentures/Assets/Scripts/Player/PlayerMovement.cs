using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 8f;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void LateUpdate()
    {
        if (playerInput.isMovingLeft)
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        else if (playerInput.isMovingRight)
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    public void UpdateMoveSpeed(float delta)
    {
        moveSpeed += delta;
    }
}
