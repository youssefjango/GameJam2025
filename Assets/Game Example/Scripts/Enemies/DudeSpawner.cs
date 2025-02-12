using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicEnemySpawner2D : MonoBehaviour
{
    public GameObject enemyPrefab; // Enemy prefab to spawn
    public Transform player;       // Reference to the player
    public float minDistance = 4.8f; // Minimum spawn distance
    public float maxDistance = 5.8f; // Maximum spawn distance
    public float spawnInterval = 2f; // Time interval between spawns in seconds
    private bool isSpawning = true; // Toggle to control spawning

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // Wait for the interval before spawning again
        }
    }

    void SpawnEnemy()
    {
        float randomOffsetX = UnityEngine.Random.Range(minDistance, maxDistance);
        float randomOffsetY = UnityEngine.Random.Range(minDistance, maxDistance);

        randomOffsetX *= UnityEngine.Random.Range(0, 2) == 0 ? 1 : -1;
        randomOffsetY *= UnityEngine.Random.Range(0, 2) == 0 ? 1 : -1;

        Vector2 spawnPosition = new Vector2(
            player.position.x + randomOffsetX,
            player.position.y + randomOffsetY
        );

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void StopSpawning()
    {
        isSpawning = false; // Stop the coroutine
    }
}
