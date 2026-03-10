using UnityEngine;

// Questo componente vive sul prefab Chunk.
// Responsabilità unica: esporre i dati strutturali del chunk
// allo spawner. Zero logica di gameplay qui.
public class ChunkController : MonoBehaviour
{
    // SerializeField: privato per il codice, visibile in Inspector.
    // Assegni i riferimenti direttamente nel prefab, una volta sola. 
    [SerializeField] private Transform _connectPoint;
    [SerializeField] private Transform[] _obstacleSpawnPoints;

    // Proprietà read_only: lo spawner legge non scrive
    public Transform ConnectPoint => _connectPoint;
    public Transform[] ObstacleSpawnPoints => _obstacleSpawnPoints;

    // Chiamato dal TilePool quando il chunk viene restituito al pool.
    // Resetta lo stato (utile quando aggiungi ostacoli dinamici).
    public void ResetChunk()
    {
        // ...
    }

}
