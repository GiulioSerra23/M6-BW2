using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager2 : MonoBehaviour
{
    public static InventoryManager2 instance;
    public int coins;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
