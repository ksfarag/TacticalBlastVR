using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemyAI  enemyPrefab;
    [SerializeField] private float    spawnInterval;
    [SerializeField] private Player   player;
    [SerializeField] private int      maxEnemiesCount;

    private List<EnemyAI>Enemies = new List<EnemyAI>();
    private float timeSinceLastSpawn;
    private void Start()
    {
        timeSinceLastSpawn = spawnInterval;
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > spawnInterval)
        {
            timeSinceLastSpawn= 0;
            if (Enemies.Count < maxEnemiesCount)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        EnemyAI enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        int spawnPointIndex = Enemies.Count % spawnPoints.Length;
        enemy.Init(player, spawnPoints[spawnPointIndex]);
        Enemies.Add(enemy);
    }
}
