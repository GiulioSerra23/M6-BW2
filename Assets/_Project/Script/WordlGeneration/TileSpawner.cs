using UnityEngine;
using System.Collections.Generic;

// TileSpawner: unica responsabilità → decidere QUANDO e DOVE
// spawnare chunk. Non gestisce il pool (delega a TilePool),
// non gestisce ostacoli (delega a ObstacleSpawner).
public class TileSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TilePool _tilePool;

    [Header("Settings")]
    // Quanti chunk teniamo attivi contemporaneamente.
    // 5 è sufficiente per coprire la view distance senza sprechi.
    [SerializeField] private int _chunksVisibleAhead = 5;

    // Lunghezza di un singolo chunk sull'asse Z.
    // Deve corrispondere alla scala del tuo plane.
    // [SerializeField] private float _chunkLength = 20f;

    // Riferimento all'ultimo ConnectPoint spawnato,
    // così sappiamo dove attaccare il prossimo chunk.
    private Transform _lastConnectPoint;

    // Lista dei chunk attualmente attivi in scena.
    // Usiamo Queue: il primo entrato è il primo da rimuovere (FIFO).
    private readonly Queue<ChunkController> _activeChunks = new();

    private void Start()
    {
        // Spawn iniziale: riempiamo il "corridoio" davanti al player.
        for (int i = 0; i < _chunksVisibleAhead; i++)
            SpawnChunk();
    }

    private void Update()
    {
        // Ogni frame: se il numero di chunk attivi è sotto la soglia,
        // spawna un nuovo chunk e rimuovi il più vecchio.
        // 
        // In realtà qui useremo la distanza del player,
        // ma per ora questa versione base è sufficiente per il test.
        if (_activeChunks.Count < _chunksVisibleAhead)
            SpawnChunk();
    }

    private void SpawnChunk()
    {
        // Chiede al pool un chunk disponibile.
        var chunk = _tilePool.GetChunk();

        // Posizionamento: se è il primo chunk, parte dall'origine.
        // Altrimenti si attacca al ConnectPoint dell'ultimo chunk.
        Vector3 spawnPos = _lastConnectPoint != null
            ? _lastConnectPoint.position
            : Vector3.zero;

        chunk.transform.position = spawnPos;
        _lastConnectPoint = chunk.ConnectPoint;

        _activeChunks.Enqueue(chunk);

        // Se abbiamo troppi chunk attivi, restituiamo il più vecchio al pool.
        if (_activeChunks.Count > _chunksVisibleAhead)
        {
            var old = _activeChunks.Dequeue();
            _tilePool.ReturnChunk(old);
        }
    }
}