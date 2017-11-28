using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public int firstLevelRockAmount, rocksAddedPerLvl, rockAmountMax, rockResourceMin, rockResourceMinAdded, rockResourceMax, rockResourceMaxAdded;
    public GameObject enemySpawner1, enemySpawner2, enemySpawner3, enemySpawner4, rockPrefab;
    public float xMin, xMax, yMin, yMax;

    private List<GameObject> enemySpawners = new List<GameObject>();
    private List<GameObject> rocks = new List<GameObject>();

    private int currentRockAmount;

    public bool generating;

    public void Start()
    {
        currentRockAmount = firstLevelRockAmount;
        GenerateLevel(currentRockAmount, rockResourceMin, rockResourceMax);
        enemySpawners.Add(enemySpawner1);
        enemySpawners.Add(enemySpawner2);
        enemySpawners.Add(enemySpawner3);
        enemySpawners.Add(enemySpawner4);
    }

    private void FixedUpdate()
    {
        //CHECK ALL THE ROCKS
        if (generating)
        {
            Debug.Log("starting with new generation");
            foreach (GameObject rock in rocks)
            {
                //int infiniteLoopPrevention = 0;
                if (rock.GetComponent<SpaceRockController>().collideWithRock || rock.GetComponent<SpaceRockController>().collideWithPlate)
                {
                    rock.transform.position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), -3f);
                    //infiniteLoopPrevention++;
                    //if (infiniteLoopPrevention > 600000)
                    //{
                    //    Debug.Log("Fuck it, ABBANDON SHIP!!!");
                    //    infiniteLoopPrevention = 0;
                    //    break;
                    //}
                }
                else
                {
                    rock.GetComponent<SpaceRockController>().wellPlaced = true;
                }
            }

            int rocksWellPlaced = 0;
            foreach (GameObject rock in rocks)
            {
                if (rock.GetComponent<SpaceRockController>().wellPlaced)
                {
                    rocksWellPlaced++;
                }
            }

            if (rocksWellPlaced >= rocks.Count)
            {
                generating = false;
                Debug.Log("rocksWellPlaced: " + rocksWellPlaced);
                Debug.Log("rocks.Count: " + rocks.Count);
            }
        }
    }

    public void NextLevel()
    {
        GameObject.Find("Weapon").GetComponent<RandomWeaponController>().GiveRandomWeapon();

        //Add more rocks and resources per rocks to the level
        if (currentRockAmount < rockAmountMax)
        {
            currentRockAmount += rocksAddedPerLvl;
        }
        rockResourceMin += rockResourceMinAdded;
        rockResourceMax += rockResourceMaxAdded;
        GenerateLevel(currentRockAmount, rockResourceMin, rockResourceMax);
    }

    public void GenerateLevel(int totalrockNumber, int resourceMin, int resourceMax)
    {
        Removerocks();
        
        //PUT DOWN ALL THE ROCKS
        for (int iRock = 0; iRock < totalrockNumber; iRock++)
        {
            Vector3 pos = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
            GameObject rock = Instantiate(rockPrefab, pos, Quaternion.identity);

            rock.GetComponent<SpaceRockController>().minResourceAmount = resourceMin;
            rock.GetComponent<SpaceRockController>().maxResourceAmount = resourceMax;

            rocks.Add(rock);
        }

        foreach (GameObject enemySpawner in enemySpawners)
        {
            enemySpawner.GetComponent<EnemySpawner>().giveEnemySpeed += 0.5f;
            enemySpawner.GetComponent<EnemySpawner>().spawnRate *= 0.8f;
            if (totalrockNumber >= 18)
            {
                enemySpawner.GetComponent<EnemySpawner>().redEnemyActive = true;
            }
            enemySpawner.GetComponent<EnemySpawner>().KillAllEnemys();
        }

        StartCoroutine("WaitForRockCollisionCheck");
        generating = true;
    }

    IEnumerator WaitForRockCollisionCheck()
    {
        Debug.Log("ik ben even aan het wachten op kut steentjes");
        yield return new WaitForSeconds(2f);
    }

    public void Removerocks()
    {
        if (rocks.Count != 0)
        {
            for (int iRock = rocks.Count - 1; iRock >= 0; iRock--)
            {
                Destroy(rocks[iRock]);
            }
            rocks.Clear();
        }
    }
}
