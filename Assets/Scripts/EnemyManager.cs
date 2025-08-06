using System.Threading.Tasks;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Public Variables
    
    public const int MaxEnemies = 10;
    public static EnemyManager Instance;          // Singleton
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnRadius = 5f;

    #endregion

    #region Private Variables
    
    private OrbitAroundPlayer orbit;
    private float enemySpawnTime;
    private float totalScore = 0f;
    private int currentEnemies = 0;
    
    #endregion
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        SpawnEnemy();
        //if (orbit) orbit.enabled = true;
    }
    public void SpawnEnemy()
    {
        if(currentEnemies >= MaxEnemies)
        {
            GameOver();
            return;
        }

        Vector3 spawnDirection;
        float verticalDot;
        int attempts = 0;

        do
        {
            spawnDirection = Random.onUnitSphere.normalized;
            verticalDot = Vector3.Dot(spawnDirection, Vector3.up);
            attempts++;
            if (attempts > 100) break;
        } while (Mathf.Abs(verticalDot) > 0.25f); // Avoid up/down

        Vector3 spawnPosition = player.position + spawnDirection * spawnRadius;

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        orbit = enemy.GetComponent<OrbitAroundPlayer>();

        if (orbit)  orbit.player = player;
        enemySpawnTime = Time.time;
        currentEnemies++;
    }

    public async void EnemyDied()
    {
        float timeTaken = Time.time - enemySpawnTime;  // Stop timer
        totalScore += timeTaken;

        Debug.Log($"Enemy killed in {timeTaken:F2} seconds. Total score: {totalScore:F2}");

        await Task.Delay(1500);
        SpawnEnemy();
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
