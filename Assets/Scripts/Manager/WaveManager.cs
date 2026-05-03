using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int initialEnemyCount = 3;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float waveDelay = 5f;
    
    private int currentWave = 1;
    private int enemiesSpawned;
    private int maxEnemiesPerWave;
    private List<Enemy> activeEnemies = new List<Enemy>();
    private bool waveActive = false;
    private float nextSpawnTime;
    private float nextWaveTime;

    private void Start()
    {
        maxEnemiesPerWave = initialEnemyCount;
        StartWave();
    }

    private void Update()
    {
        if (!waveActive)
        {
            if (Time.time >= nextWaveTime)
            {
                StartWave();
            }
            return;
        }

        // Remove dead enemies
        activeEnemies.RemoveAll(e => e == null || !e.IsAlive);

        // Spawn enemies
        if (enemiesSpawned < maxEnemiesPerWave && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }

        // Check if wave is complete
        if (activeEnemies.Count == 0 && enemiesSpawned >= maxEnemiesPerWave)
        {
            CompleteWave();
        }
    }

    private void StartWave()
    {
        currentWave++;
        maxEnemiesPerWave = initialEnemyCount + (currentWave - 1) * 2;
        enemiesSpawned = 0;
        waveActive = true;
        nextSpawnTime = Time.time;
        Debug.Log($"Wave {currentWave} started! Enemies: {maxEnemiesPerWave}");
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyObject = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        
        if (enemy != null)
        {
            activeEnemies.Add(enemy);
        }
        
        enemiesSpawned++;
    }

    private void CompleteWave()
    {
        waveActive = false;
        nextWaveTime = Time.time + waveDelay;
        Debug.Log($"Wave {currentWave} complete! Next wave in {waveDelay}s");
    }

    public int GetCurrentWave => currentWave;
    public int GetActiveEnemyCount => activeEnemies.Count;
}