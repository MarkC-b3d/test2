using UnityEngine;
using System.Collections;

public class BallSpawner : MonoBehaviour
{
    // Reference to the ball prefab
    public GameObject ballPrefab;

    // Define the area where balls will spawn
    public Vector3 spawnAreaCenter;
    public Vector3 spawnAreaSize;

    // Spawn delay in seconds
    public float spawnDelay = 1f;

    // Maximum number of balls allowed in the scene
    public int maxBalls = 20;

    private void Start()
    {
        if (ballPrefab == null)
        {
            UnityEngine.Debug.LogError("Ball prefab is not assigned. Please assign a valid prefab in the Inspector.");
            return; // Exit if prefab is not assigned
        }

        // Start the coroutine to spawn balls
        StartCoroutine(SpawnBalls());
    }

    // Coroutine to spawn balls
    private IEnumerator SpawnBalls()
    {
        while (true) // Infinite loop
        {
            // Count the number of active balls in the scene
            int ballCount = GameObject.FindGameObjectsWithTag("Ball").Length;

            if (ballCount < maxBalls)
            {
                // Spawn a ball if the count is below the maximum
                SpawnBall();
            }

            // Wait before spawning the next ball
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // Method to spawn a ball at a random position within the spawn area
    private void SpawnBall()
    {
        if (ballPrefab == null)
        {
            UnityEngine.Debug.LogError("Ball prefab is not assigned.");
            return; // Exit if prefab is not assigned
        }

        // Generate a random position within the defined area
        Vector3 randomPosition = spawnAreaCenter + new Vector3(
            UnityEngine.Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            UnityEngine.Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            UnityEngine.Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        // Instantiate the ball prefab at the random position
        GameObject newBall = Instantiate(ballPrefab, randomPosition, Quaternion.identity);

        // Set a tag for the ball to count later
        newBall.tag = "Ball";
    }

    // Optional: Draw the spawn area in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}
