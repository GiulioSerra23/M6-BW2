using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup2 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager2.instance.coins += 1;

            Debug.Log("Monete attuali: " + InventoryManager2.instance.coins);

            Destroy(gameObject);
        }
    }
}
