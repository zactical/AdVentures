using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 8f;
    [SerializeField]
    private GameObject leftBound;
    [SerializeField]
    private GameObject rightBound;
    [SerializeField]
    private GameObject topBound;
    [SerializeField]
    private GameObject bottomBound;

    private PlayerInput playerInput;
    private Vector3 movementVector = new Vector3(0, 0, 0);

    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void LateUpdate()
    {
        if (CanMove == false)
            return;


        if (playerInput.isMovingLeft && transform.position.x > leftBound.transform.position.x)
            movementVector.x = -1;
        // transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        else if (playerInput.isMovingRight && transform.position.x < rightBound.transform.position.x)
            movementVector.x = 1;
        else
            movementVector.x = 0;

        if (playerInput.isMovingUp && transform.position.y < topBound.transform.position.y)
            movementVector.y = 1;
        else if (playerInput.isMovingDown && transform.position.y > bottomBound.transform.position.y)
            movementVector.y = -1;
        else
            movementVector.y = 0;

        if(movementVector.x != 0 || movementVector.y != 0)
            transform.Translate(movementVector * Time.deltaTime * moveSpeed);
    }

    public void UpdateMoveSpeed(float delta)
    {
        moveSpeed += delta;
    }

    public void ToggleCanMove(bool enabled)
    {
        CanMove = enabled;
    }
}
