using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayerChoise : MonoBehaviour
{
    [SerializeField] private GameObject narutoPrefab;
    [SerializeField] private GameObject sasukePrefab;
    [SerializeField] private Transform spawnPoint;


    private void Awake()
    {
        GameObject capsulePlayer = GameObject.FindWithTag("Player");

        if (capsulePlayer != null)
        {
            spawnPoint = capsulePlayer.transform;

            MeshRenderer mr = capsulePlayer.GetComponent<MeshRenderer>();
            if (mr != null) mr.enabled = false;
        }
        else
        {
            Debug.LogWarning("Nessun player trovato");
        }
        SpawnSelectedCharacter();
    }


    private void SpawnSelectedCharacter()
    {
        string choise = PlayerSelection.SelectedCharacter;
        GameObject prefabToIstantiate = null;

        if (choise == "Naruto")
        {
            prefabToIstantiate = narutoPrefab;
        }
        else if (choise == "Sasuke")
        {
            prefabToIstantiate = sasukePrefab;
        }
        else
        {
            Debug.LogWarning("Nessuna scelta fatta. Di default scelgo Naruto");
            prefabToIstantiate = narutoPrefab;
        }

        if (prefabToIstantiate != null && spawnPoint != null)
        {
            GameObject playerClone = Instantiate(prefabToIstantiate, spawnPoint.position, spawnPoint.rotation);

            PlayerMotor playerMotor = playerClone.GetComponent<PlayerMotor>();
            PlayerManager.Instance.SetPlayer(playerMotor);

            Debug.Log("Player spawnato" + choise);
        }
    }
    //private void Start()
    //{
    //    string choise = PlayerSelection.SelectedCharacter;

    //    if (choise == "Naruto")
    //    {
    //        Instantiate(narutoPrefab, spawnPoint.position, spawnPoint.rotation);
    //    }
    //    else if (choise == "Sasuke")
    //    {
    //        Instantiate(sasukePrefab, spawnPoint.position, spawnPoint.rotation);
    //    }
    //    else
    //    {
    //        Debug.Log("Nessun personaggio scelto, carico naruto di default");
    //        Instantiate(narutoPrefab, spawnPoint.position, spawnPoint.rotation);
    //    }
    //}
}
