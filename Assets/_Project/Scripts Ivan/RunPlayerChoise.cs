using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayerChoise : MonoBehaviour
{
    [SerializeField] private GameObject narutoPrefab;
    [SerializeField] private GameObject sasukePrefab;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        string choise = PlayerSelection.SelectedCharacter;

        if (choise == "Naruto")
        {
            Instantiate(narutoPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else if (choise == "Sasuke")
        {
            Instantiate(sasukePrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.Log("Nessun personaggio scelto, carico naruto di default");
            Instantiate(narutoPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
