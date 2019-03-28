using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Enemy enemy;
    private Animator physicsAnimator;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        physicsAnimator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    public void FlyAway(float delay)
    {
        var triggerToSet = Random.Range(0, 100) > 50 ? "flyAwayLeft" : "flyAwayRight";

      //  var delay = Random.Range(0f, 1f);

        StartCoroutine(SetTriggerAfterDelay(delay, triggerToSet));
    }

    public void OnFlyAwayFinished()
    {
        enemy.Kill(false);
    }

    private IEnumerator SetTriggerAfterDelay(float delay, string triggerName)
    {        
        yield return new WaitForSeconds(delay);
        physicsAnimator.SetTrigger(triggerName);
    }
}
