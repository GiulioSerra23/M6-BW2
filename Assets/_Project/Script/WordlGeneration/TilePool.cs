using UnityEngine;
using System.Collections.Generic;

// TilePool: responsabilità unica → gestire il ciclo di vita
// dei chunk (crea, presta, recupera). 
// Il requisito tecnico del brief è esplicitamente Object Pooling.
public class TilePool : MonoBehaviour
{
    [SerializeField] private ChunkController _chunkPrefab;

    // Dimensione iniziale del pool. 
    // Regola: deve essere >= _chunksVisibleAhead + 2 per evitare
    // allocazioni runtime (che causano GC spike).
    [SerializeField] private int _initialPoolSize = 8;

    private readonly Queue<ChunkController> _pool = new();

    private void Awake()
    {
        // Pre-allocazione: creiamo tutti i chunk subito,
        // disattivati, pronti per essere prestati.
        for (int i = 0; i < _initialPoolSize; i++)
        {
            var chunk = Instantiate(_chunkPrefab, transform);
            chunk.gameObject.SetActive(false);
            _pool.Enqueue(chunk);
        }
    }

    public ChunkController GetChunk()
    {
        // Se il pool è vuoto (caso limite), allochiamo on-demand
        // e logghiamo un warning: significa che _initialPoolSize è troppo basso.
        if (_pool.Count == 0)
        {
            Debug.LogWarning("[TilePool] Pool esaurito — aumenta _initialPoolSize.");
            var newChunk = Instantiate(_chunkPrefab, transform);
            return Activate(newChunk);
        }

        return Activate(_pool.Dequeue());
    }

    public void ReturnChunk(ChunkController chunk)
    {
        chunk.ResetChunk();
        chunk.gameObject.SetActive(false);
        _pool.Enqueue(chunk);
    }

    private ChunkController Activate(ChunkController chunk)
    {
        chunk.gameObject.SetActive(true);
        return chunk;
    }
}
