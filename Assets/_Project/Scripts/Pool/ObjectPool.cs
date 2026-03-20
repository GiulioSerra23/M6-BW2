
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool: MonoBehaviour
{
    [SerializeField] private PoolableObject _prefab;
    [SerializeField] private int _poolSize = 20;
    [SerializeField] private int _maxPoolSize = 40;
    [SerializeField] private bool _expandable = true;

    private Queue<PoolableObject> _available;
    private List<PoolableObject> _objects = new List<PoolableObject>();

    private void Awake()
    {
        _available = new Queue<PoolableObject>(_poolSize);

        for (int i = 0; i < _poolSize; i++)
        {
            CreateObject();
        }
    }

    private PoolableObject CreateObject()
    {
        PoolableObject obj = Instantiate(_prefab, transform);
        obj.gameObject.SetActive(false);

        obj.SetPool(this);

        _available.Enqueue(obj);
        _objects.Add(obj);
        return obj;
    }

    public PoolableObject GetObject()
    {
        if (_available.Count == 0)
        {
            if (!_expandable) return null;

            if (_objects.Count >= _maxPoolSize) return null;

            CreateObject();
        }

        PoolableObject obj = _available.Dequeue();
        obj.gameObject.SetActive(true);
        obj.OnSpawned();
        return obj;
    }

    public void ReturnToPool(PoolableObject obj)
    {
        obj.OnDespawned();

        if (_available.Count > _poolSize)
        {
            _objects.Remove(obj);
            Destroy(obj.gameObject);
            return;
        }

        obj.transform.SetParent(transform);
        obj.gameObject.SetActive(false);
        _available.Enqueue(obj);
    }
}