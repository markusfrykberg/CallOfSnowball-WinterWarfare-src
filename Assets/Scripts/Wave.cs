using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour
{
    public Enemy[] enemies;
    private int enemyNumber;
    private bool running;
    private double lastEnemyTime;

	private Vector3 spawnPosition;

    public void Start()
    {
        running = false;
        enemyNumber = 0;
		spawnPosition = transform.parent.position;
    }

    public void Update()
    {
        if (running && Time.time - 0.7 > lastEnemyTime) {
            Enemy nextEnemy = NextEnemy();
            if (nextEnemy == null) {
                running = false;
            } else {
           //     Vector3 spawnPosition = GameObject.FindWithTag("EnemySpawner")
           //         .transform.position;
                Instantiate(nextEnemy, spawnPosition, Quaternion.identity);
                lastEnemyTime = Time.time;
            }
        }
    }

    public void Run()
    {
        running = true;
    }

    public Enemy NextEnemy()
    {
        Enemy enemy = null;
        if (enemyNumber < enemies.Length) {
            enemy = enemies[enemyNumber];
            enemyNumber++;
        }
        return enemy;
    }
}
