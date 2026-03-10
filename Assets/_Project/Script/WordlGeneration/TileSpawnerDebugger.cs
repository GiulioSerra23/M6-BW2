using UnityEngine;
using System.Collections.Generic;

// Attach questo componente sullo stesso GameObject di TileSpawner.
// Rimuovilo o disattivalo prima della build finale.
public class TileSpawnerDebugger : MonoBehaviour
{
    [SerializeField] private TileSpawner _tileSpawner;
    [SerializeField] private TilePool    _tilePool;

    // Premi G in Play Mode per triggerare il report manualmente
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            RunDiagnostic();
    }

    private void Start()
    {
        // Report automatico 1 secondo dopo lo Start,
        // così i chunk hanno il tempo di spawnare.
        Invoke(nameof(RunDiagnostic), 1f);
    }

    private void RunDiagnostic()
    {
        Debug.Log("===== TILE SPAWNER DIAGNOSTIC =====");

        // --- 1. Quanti chunk attivi ---
        var activeChunks = new List<ChunkController>(
            FindObjectsByType<ChunkController>(FindObjectsSortMode.None)
        );
        int activeCount = 0;
        foreach (var c in activeChunks)
            if (c.gameObject.activeSelf) activeCount++;

        Debug.Log($"[1] Chunk attivi in scena: {activeCount}");

        // --- 2. Sono in fila o si sovrappongono? ---
        // Ordiniamo per posizione Z e controlliamo la distanza tra chunk consecutivi
        activeChunks.RemoveAll(c => !c.gameObject.activeSelf);
        activeChunks.Sort((a, b) => a.transform.position.z.CompareTo(b.transform.position.z));

        bool overlapping = false;
        for (int i = 1; i < activeChunks.Count; i++)
        {
            float gap = activeChunks[i].transform.position.z
                      - activeChunks[i - 1].transform.position.z;

            // Se il gap è < 0.1f probabilmente si sovrappongono
            string status = gap < 0.1f ? "⚠ SOVRAPPOSTI" : "✓ OK";
            Debug.Log($"[2] Chunk {i-1}→{i} | gap Z: {gap:F2} | {status}");

            if (gap < 0.1f) overlapping = true;
        }

        if (!overlapping && activeChunks.Count > 1)
            Debug.Log("[2] Tutti i chunk sono in fila correttamente.");

        // --- 3. ConnectPoint configurato? ---
        foreach (var c in activeChunks)
        {
            if (c.ConnectPoint == null)
                Debug.LogError($"[3] ⚠ ConnectPoint MANCANTE su: {c.gameObject.name}");
            else
                Debug.Log($"[3] {c.gameObject.name} → ConnectPoint @ Z={c.ConnectPoint.position.z:F2}");
        }

        // --- 4. Riferimenti Inspector ---
        if (_tileSpawner == null)
            Debug.LogError("[4] TileSpawner NON assegnato nel Debugger!");
        if (_tilePool == null)
            Debug.LogError("[4] TilePool NON assegnato nel Debugger!");

        Debug.Log("===== FINE DIAGNOSTIC =====");
    }
}