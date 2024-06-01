using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnRadious = 6f;
    public int enemiesToSpawn = 6;

    private Animator Animator;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SpawnEnemies();
            Assets.ScoreManager.Instance.AddScore(100);
        }
    }

    private void SpawnEnemies()
    {
        Animator.SetBool("Activated", true);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            float angle = i * Mathf.PI * 2 / enemiesToSpawn;
            Vector3 enemyPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * spawnRadious + spawnPoint.position;
            GameObject enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
            
            ZombieController zombieController = enemy.GetComponent<ZombieController>();

            if (zombieController != null)
            {
                zombieController.SetPlayerReference(GameObject.Find("Player").transform);
            }
            else
            {
                Debug.LogError("ZombieController not found in enemy prefab");
            }
        }
        
        Animator.SetBool("Activated", false);
    }
}