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
        physicsAnimator.keepAnimatorControllerStateOnDisable = true;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        transform.localPosition = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var damageableObject = collider.gameObject.GetComponent<ITakeDamage>();

        if (damageableObject != null)
        {
           // physicsAnimator.
            physicsAnimator.SetTrigger("exit");
            physicsAnimator.ResetTrigger("flyAwayLeft");
            physicsAnimator.ResetTrigger("flyAwayRight");
            physicsAnimator.Update(Time.deltaTime);
            StopAllCoroutines();
            damageableObject.TakeDamage(1);
            enemy.Kill(true);
        }
    }

    public void FlyAway(float delay)
    {
        var triggerToSet = Random.Range(0, 100) > 50 ? "flyAwayLeft" : "flyAwayRight";

      //  var delay = Random.Range(0f, 1f);

        StartCoroutine(SetTriggerAfterDelay(delay, triggerToSet));
    }

    public void OnFlyAwayFinished()
    {
        physicsAnimator.SetTrigger("exit");
        physicsAnimator.ResetTrigger("flyAwayLeft");
        physicsAnimator.ResetTrigger("flyAwayRight");
        physicsAnimator.Update(Time.deltaTime);
        StopAllCoroutines();
        enemy.Kill(false);
    }

    private IEnumerator SetTriggerAfterDelay(float delay, string triggerName)
    {        
        yield return new WaitForSeconds(delay);
        physicsAnimator.SetTrigger(triggerName);
    }
}
