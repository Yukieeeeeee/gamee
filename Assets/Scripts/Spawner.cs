using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;
    [Range(0,1)] public float obstacleSpawnTimer = 0.2f;
    [Range(0,1)] public float obstacleSpeedTimer = 0.2f;
    public float obstacleSpawnTime = 2f;
    public float obstacleSpeed = 5f;
    private float timeUntilObstacleSpawn;
    private float timeAlive;
    private float _obstacleSpawnTime;
    private float _obstacleSpeed;
    

    private void Start()
    {
        GameManager.Instance.OnGameOver.AddListener(ClearObstacles);
        GameManager.Instance.OnPlay.AddListener(ResetTimers);
    }

    void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            timeAlive += Time.deltaTime;

            CalculateTimers();

            SpawnLoop();
        }
    }

    private void SpawnLoop() {

        timeUntilObstacleSpawn += Time.deltaTime;
        if (timeUntilObstacleSpawn >= _obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
        
    }
    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];


        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        spawnedObstacle.transform.parent = obstacleParent;


        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.linearVelocity = Vector2.left * _obstacleSpeed;


    }

    private void ClearObstacles() {

        foreach (Transform child in obstacleParent) {
        Destroy(child.gameObject);
        }

    }

    private void ResetTimers() {

        timeAlive = 1f;
        _obstacleSpawnTime = obstacleSpawnTime;
        _obstacleSpeed = obstacleSpeed;
}
    private void CalculateTimers() {
        _obstacleSpawnTime = obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimer);
        _obstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedTimer);
    }

}
