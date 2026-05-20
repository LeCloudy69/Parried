using UnityEngine;
using System.Collections;
using TMPro; // Needed for Text Mesh Pro UI

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName = "Wave 1";
        public int enemyCount = 3;
        public float timeBetweenSpawns = 1f;
        public GameObject enemyPrefab; // Can have different enemies per wave!
    }

    [Header("Configuration")]
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 3f;

    [Header("UI References")]
    public TMP_Text waveText; // Drag a TextMeshPro object here
    
    private int currentWaveIndex = 0;
    private bool isWaveActive = false;

    // Add this variable at the top
    public GameFlow gameFlow; 

    void Start()
    {
        ShootingEnemy.enemyCount = 0; // Reset enemy count at start

        // Start the game loop
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    void Update()
    {
        if (isWaveActive)
        {
            // Check if all enemies are dead
            if (ShootingEnemy.enemyCount <= 0)
            {
                WaveCompleted();
            }
        }
    }

    void WaveCompleted()
    {
        isWaveActive = false;
        
        if (currentWaveIndex + 1 < waves.Length)
        {
            currentWaveIndex++;
            StartCoroutine(StartNextWave(currentWaveIndex));
        }
        else
        {
            // WIN CONDITION
            if(gameFlow != null) gameFlow.TriggerVictory();
        }
    }

    IEnumerator StartNextWave(int index)
    {
        // Show Wave Name
        if(waveText != null) 
        {
            waveText.text = "Get Ready...";
            yield return new WaitForSeconds(2f); // Breathing room
            waveText.text = waves[index].waveName;
        }

        yield return new WaitForSeconds(timeBetweenWaves);

        // Spawn Enemies
        Wave currentWave = waves[index];
        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            SpawnEnemy(currentWave.enemyPrefab);
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }

        // Check wave is active
        isWaveActive = true;
    }

    void SpawnEnemy(GameObject prefab)
    {
        // Pick random spawn point
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(prefab, spawn.position, spawn.rotation);
    }
}