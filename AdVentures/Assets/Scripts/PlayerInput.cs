using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isMovingLeft;
    public bool isMovingRight;

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        isMovingLeft = Input.GetKey(KeyCode.LeftArrow);
        isMovingRight = Input.GetKey(KeyCode.RightArrow);
    }
}
