
using UnityEngine;

public class PickupSpawnPoint : MonoBehaviour
{
    [SerializeField] private PoolType poolType;

    public void Spawn()
    {
        PoolableObject obj = PoolManager.Instance.GetPool(poolType).Get();

        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }
}

