using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Enemy enemy;
    private Animator physicsAnimator;

    private int exitAnimationHash;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        physicsAnimator = GetComponentInChildren<Animator>();
        physicsAnimator.keepAnimatorControllerStateOnDisable = true;
        exitAnimationHash = Animator.StringToHash("exit");
    }

    private void OnEnable()
    {
        physicsAnimator.SetTrigger(exitAnimationHash);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var damageableObject = collider.gameObject.GetComponent<ITakeDamage>();

        if (damageableObject != null)
        {
            physicsAnimator.SetTrigger(exitAnimationHash);
            damageableObject.TakeDamage(1);
            enemy.Kill(true);
        }
    }

    public void FlyAway(float delay)
    {
        var triggerToSet = Random.Range(0, 100) > 50 ? "flyAwayLeft" : "flyAwayRight";
        StartCoroutine(SetTriggerAfterDelay(delay, triggerToSet));
    }

    public void OnFlyAwayFinished()
    {
        physicsAnimator.SetTrigger(exitAnimationHash);
        enemy.Kill(false);
    }

    private IEnumerator SetTriggerAfterDelay(float delay, string triggerName)
    {        
        yield return new WaitForSeconds(delay);
        physicsAnimator.SetTrigger(triggerName);
    }
}
