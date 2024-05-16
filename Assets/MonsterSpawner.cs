using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Transform spawnPoint;
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            SpawnMonster();
    }

    private void SpawnMonster()
    {
        GameObject monster = Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);

        // Get the ZombieController component from the spawned monster
        ZombieController zombieController = monster.GetComponent<ZombieController>();

        // Set the player's transform reference in the ZombieController
        if (zombieController != null)
        {
            zombieController.SetPlayerReference(playerTransform);
        }
        else
        {
            Debug.LogWarning("ZombieController component not found on the spawned monster!");
        }
    }
}
