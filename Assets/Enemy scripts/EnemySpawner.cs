using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerTransform;
    public float spawnDistance = 10f;
    public float spawnInterval = 5f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Get the ZombieController component from the spawned enemy
        ZombieController zombieController = enemy.GetComponent<ZombieController>();

        // Set the player's transform reference in the ZombieController
        if (zombieController != null)
        {
            zombieController.SetPlayerReference(playerTransform);
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
