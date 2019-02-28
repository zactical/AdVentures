using System;
using System.Collections;
using UnityEngine;

public class PooledMonoBehaviour : MonoBehaviour
{
    [SerializeField]
    private int initialPoolSize = 50;

    public event Action<PooledMonoBehaviour> OnReturnToPool;

    public int InitialPoolSize { get { return initialPoolSize; } }

    protected virtual void OnDisable()
    {
        if (OnReturnToPool != null)
            OnReturnToPool(this);
    }

    public T Get<T>(bool enable = true, GameObject poolParent = null) where T : PooledMonoBehaviour
    {
        var pool = Pool.GetPool(this, poolParent);
        var pooledObject = pool.Get<T>();

        if(enable)
            pooledObject.gameObject.SetActive(true);

        return pooledObject;
    }

    public T Get<T>(Vector3 position, Quaternion rotation, GameObject poolParent = null) where T : PooledMonoBehaviour
    {
        var pooledObject = Get<T>(poolParent: poolParent);
        pooledObject.transform.position = position;
        pooledObject.transform.rotation = rotation;

        return pooledObject;
    }    

    protected virtual void ReturnToPool(float delay = 0)
    {
        if (delay == 0)
        {
            ReturnToPool();
            return;
        }

        StartCoroutine(ReturnToPoolAfterSeconds(delay));
    }

    private IEnumerator ReturnToPoolAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
