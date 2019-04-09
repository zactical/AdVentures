using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform target;
    public float timeToTarget = 3f;
    public float closenessThreshold = .1f;

    private bool canMove;
    private bool hasCalledCallback;
    private Transform startingPosition;
    private float t;
    private Vector3 targetPosition;

    private Action onFinishMovingCallback;

    private void Awake()
    {
        if (target != null)
            targetPosition = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == false)
            return;

        t += Time.deltaTime / timeToTarget;
        transform.position = Vector3.Lerp(startingPosition.position, targetPosition, t);

        if (Vector3.Distance(transform.position, targetPosition) < closenessThreshold)
        {
            canMove = false;
            if (onFinishMovingCallback != null && hasCalledCallback == false)
            {
                hasCalledCallback = true;
                onFinishMovingCallback();
            }
        }
    }

    public void StartMoving(Action callback = null)
    {
        onFinishMovingCallback = callback;
        startingPosition = gameObject.transform;
        canMove = true;
        hasCalledCallback = false;
    }

    public void SetTarget(Vector3 v3Target)
    {
        targetPosition = v3Target;
    }
}
