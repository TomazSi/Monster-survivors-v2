using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerTransform;
    public float spawnDistance = 10f;
    public float baseSpawnInterval = 5f;
    
    private Camera mainCamera;
    private float spawnInterval;
    private PlayerAttributes playerAttributes;


    void Start()
    {
        mainCamera = Camera.main;
        playerAttributes = playerTransform.GetComponent<PlayerAttributes>();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            UpdateSpawnInterval();
            yield return new WaitForSeconds(spawnInterval);
            int levelMultiplier = Mathf.CeilToInt(playerAttributes.Level * 1.5f); // Increase the number of enemies based on level
            for (int i = 0; i < levelMultiplier; i++)
            {
                SpawnEnemy();
            }
        }
    }

    void UpdateSpawnInterval()
    {
        spawnInterval = Mathf.Max(1f, baseSpawnInterval - (playerAttributes.Level * 0.2f)); // Decrease interval as level increases
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Configuring the enemy's damage and health
        ZombieController zombieController = enemy.GetComponent<ZombieController>();
        if (zombieController != null)
        {
            zombieController.SetPlayerReference(playerTransform);
            zombieController.damage = (2 * playerAttributes.Level); // Scale damage with player level
            zombieController.maxHealth = (10 * playerAttributes.Level); // Scale health with player level
        }
        else
        {
            Debug.LogWarning("ZombieController component not found on the spawned enemy!");
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;

        // Get the camera's view boundaries
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        // Choose a random edge (left, right, top, bottom)
        int edge = Random.Range(0, 4);
        switch (edge)
        {
            case 0: // Left
                spawnPosition = new Vector3(mainCamera.transform.position.x - camWidth / 2 - spawnDistance, Random.Range(mainCamera.transform.position.y - camHeight / 2, mainCamera.transform.position.y + camHeight / 2), 0);
                break;
            case 1: // Right
                spawnPosition = new Vector3(mainCamera.transform.position.x + camWidth / 2 + spawnDistance, Random.Range(mainCamera.transform.position.y - camHeight / 2, mainCamera.transform.position.y + camHeight / 2), 0);
                break;
            case 2: // Top
                spawnPosition = new Vector3(Random.Range(mainCamera.transform.position.x - camWidth / 2, mainCamera.transform.position.x + camWidth / 2), mainCamera.transform.position.y + camHeight / 2 + spawnDistance, 0);
                break;
            case 3: // Bottom
                spawnPosition = new Vector3(Random.Range(mainCamera.transform.position.x - camWidth / 2, mainCamera.transform.position.x + camWidth / 2), mainCamera.transform.position.y - camHeight / 2 - spawnDistance, 0);
                break;
        }

        return spawnPosition;
    }
}
