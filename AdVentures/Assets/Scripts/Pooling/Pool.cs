using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private static Dictionary<PooledMonoBehaviour, Pool> pools = new Dictionary<PooledMonoBehaviour, Pool>();

    private Queue<PooledMonoBehaviour> objects = new Queue<PooledMonoBehaviour>();

    private PooledMonoBehaviour prefab;

    public static Pool GetPool(PooledMonoBehaviour prefab, Transform poolLocation = null, GameObject poolParent = null)
    {
        return GetPool(prefab, poolLocation == null ? new Vector3() : poolLocation.position, poolParent);
    }

    public static Pool GetPool(PooledMonoBehaviour prefab, Vector3 poolLocation, GameObject poolParent = null)
    {
        if (pools.ContainsKey(prefab))
            return pools[prefab];

        var pool = new GameObject("Pool-" + prefab.name).AddComponent<Pool>();

        if (poolParent != null)
            pool.gameObject.transform.SetParent(poolParent.transform, false);

        pool.gameObject.transform.position = poolLocation;

        pool.prefab = prefab;

        pools.Add(prefab, pool);
        return pool;
    }

    public static void ClearPools()
    {
        pools.Clear();
    }

    public T Get<T>() where T : PooledMonoBehaviour
    {
        if (objects.Count == 0)
            GrowPool();

        var pooledObject = objects.Dequeue();
        return pooledObject as T;
    }

    public void WarmPool()
    {
        if (objects.Count == 0)
            GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < prefab.InitialPoolSize; i++)
        {
            var pooledObject = Instantiate(prefab) as PooledMonoBehaviour;
            pooledObject.gameObject.name += " " + i;

            pooledObject.OnReturnToPool += AddObjectToAvailableQueue;

            pooledObject.transform.SetParent(this.transform);
            pooledObject.gameObject.SetActive(false);
        }
    }

    private void AddObjectToAvailableQueue(PooledMonoBehaviour pooledObject)
    {
        pooledObject.transform.SetParent(this.transform);
        objects.Enqueue(pooledObject);
    }
}