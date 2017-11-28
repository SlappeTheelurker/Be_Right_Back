using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemy;
    public List<GameObject> enemyList = new List<GameObject>();
    public float numberOfEnemys;
    public float giveEnemySpeed;

    public float spawnRate;
    public float spawnRangeX;
    public float spawnRangeY;

    public bool redEnemyActive;

    private float nextSpawn;
    private float randX;
    private float randY;
    private Vector2 spawnLocation;

	// Use this for initialization
	void Start () {
        nextSpawn = 0f;
        redEnemyActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextSpawn)
        {
            for (int iEnemy = 0; iEnemy < numberOfEnemys; iEnemy++)
            {
                randX = Random.Range(-spawnRangeX, spawnRangeX);
                randY = Random.Range(-spawnRangeY, spawnRangeY);
                Vector2 spawnerLocation = transform.position;
                spawnLocation = spawnerLocation + new Vector2(randX, randY);

                GameObject enemySpawning = Instantiate(enemy, spawnLocation, Quaternion.identity);
                enemySpawning.GetComponent<EnemyController>().speed = giveEnemySpeed;
                if (redEnemyActive)
                {
                    if (Random.Range(0f, 1f) >= 0.8)
                    {
                        enemySpawning.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
                        enemySpawning.GetComponent<EnemyController>().attackPowerAmount = 2;
                    }
                }
                enemyList.Add(enemySpawning);
            }
            nextSpawn = Time.time + spawnRate;
        }
	}

    public void KillAllEnemys()
    {
        foreach (GameObject enemy in enemyList)
        {
            Destroy(enemy);
        }
    }
}
