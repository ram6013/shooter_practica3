using UnityEngine;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName = "Wave";
        public int enemyCount = 3;
    }
    
    [Header("Wave Configuration")]
    public Wave[] waves = new Wave[]
    {
        new Wave { waveName = "Wave 1", enemyCount = 3 },
        new Wave { waveName = "Wave 2", enemyCount = 6 },
        new Wave { waveName = "Wave 3", enemyCount = 12 }
    };
    
    [Header("Enemy Prefabs (3 Types)")]
    public GameObject[] enemyPrefabs;
    
    [Header("Spawn Settings")]
    public Transform[] spawnPoints;
    public float interWaveDelay = 3f;
    
    private int currentWaveIndex = -1;
    private bool isWaitingForNextWave = false;
    private float nextWaveTimer = 0f;
    private bool waveActive = false;
    
    void Start()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length != 3)
        {
            Debug.LogError("WaveManager requires exactly 3 enemy prefabs!");
            return;
        }
        
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.useEnemyCount = false;
        }
        
        StartNextWave();
    }
    
    void Update()
    {
        if (isWaitingForNextWave)
        {
            if (Time.time >= nextWaveTimer)
            {
                isWaitingForNextWave = false;
                StartNextWave();
            }
            return;
        }
        
        if (currentWaveIndex >= 0 && currentWaveIndex < waves.Length && waveActive)
        {
            GameObject[] aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            if (aliveEnemies.Length == 0)
            {
                waveActive = false;
                Debug.Log($"Wave {currentWaveIndex + 1} complete!");
                
                if (currentWaveIndex < waves.Length - 1)
                {
                    Debug.Log($"Waiting {interWaveDelay}s for next wave.");
                    isWaitingForNextWave = true;
                    nextWaveTimer = Time.time + interWaveDelay;
                }
                else
                {
                    Debug.Log("All waves complete! You win!");
                    GameManager gm = FindObjectOfType<GameManager>();
                    if (gm != null) gm.WinGame();
                }
            }
        }
    }
    
    void StartNextWave()
    {
        currentWaveIndex++;
        if (currentWaveIndex >= waves.Length) return;
        
        Wave wave = waves[currentWaveIndex];
        
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }
        
        Debug.Log($"Starting {wave.waveName} with {wave.enemyCount} enemies.");
        waveActive = true;
        
        for (int i = 0; i < wave.enemyCount; i++)
        {
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            
            Vector3 spawnPos = spawn.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawn.position, out hit, 5f, NavMesh.AllAreas))
            {
                spawnPos = hit.position;
            }
            
            GameObject enemy = Instantiate(prefab, spawnPos, spawn.rotation);
            enemy.tag = "Enemy";
        }
    }
}
