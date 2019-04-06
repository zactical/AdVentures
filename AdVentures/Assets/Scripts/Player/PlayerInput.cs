using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isMovingLeft;
    public bool isMovingRight;
    public bool isMovingUp;
    public bool isMovingDown;

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        isMovingLeft = Input.GetKey(KeyCode.LeftArrow);
        isMovingRight = Input.GetKey(KeyCode.RightArrow);
        isMovingUp = Input.GetKey(KeyCode.UpArrow);
        isMovingDown = Input.GetKey(KeyCode.DownArrow);
    }
}
