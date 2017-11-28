using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public List<GameObject> enemyList = new List<GameObject>();
    public float numberOfEnemys, giveEnemySpeed, timeTillWaveSpawn, spawnRangeX, spawnRangeY;

    private float spawnCounter, randX, randY;
    private Vector2 spawnLocation;

	// Use this for initialization
	void Start () {
        spawnCounter = 0f;
	}
	
	void FixedUpdate () {
        if (spawnCounter <= 0)
        {
            for (int iEnemy = 0; iEnemy < numberOfEnemys; iEnemy++)
            {
                //Get random position behind screen border
                randX = Random.Range(-spawnRangeX, spawnRangeX);
                randY = Random.Range(-spawnRangeY, spawnRangeY);
                Vector2 spawnerLocation = transform.position;
                spawnLocation = spawnerLocation + new Vector2(randX, randY);

                //Spawn enemy
                GameObject enemySpawning = Instantiate(enemy, spawnLocation, Quaternion.identity);
                enemySpawning.GetComponent<EnemyController>().speed = giveEnemySpeed;
                enemyList.Add(enemySpawning);
            }
            spawnCounter = timeTillWaveSpawn;
        }
        spawnCounter -= Time.deltaTime;
	}

    public void KillAllEnemys()
    {
        foreach (GameObject enemy in enemyList)
        {
            Destroy(enemy);
        }
    }
}
