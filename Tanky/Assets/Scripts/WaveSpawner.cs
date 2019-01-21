using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int currentWave;
        public int enemysAlive;
        public int amountToSpawn;
        public int totalEnemys;
        public Wave(int currentWave,int totalEnemys)
        {
            this.currentWave = currentWave;
            this.totalEnemys = totalEnemys;
            amountToSpawn = totalEnemys;
        }
        public void NewWave()
        {
            currentWave += 1;
            totalEnemys += 5;
            amountToSpawn = totalEnemys;
            Debug.Log("New Wave #"+ currentWave);
        }
        public bool IsFinished()
        {
            return enemysAlive == 0 && amountToSpawn == 0? true : false;
        }
        public bool CanSpawnEnemy()
        {
            return amountToSpawn > 0 ? true : false;
        }
    }
    //each wave has an aditional enemy
    public Wave wave;
    public int maxEnemysAllowed;
    public float timeBetweenSpawns;
    public GameObject enemy;
    public Transform[] spawnPoints;
    public static WaveSpawner instance = null;
    private float spawnDelayCountdown = 3f;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    void Start()
    {
        wave = new Wave(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnDelayCountdown <= 0)
        {
            spawnDelayCountdown = 3f;
            if (wave.enemysAlive < maxEnemysAllowed)
            {
                //if the amount of enemy to spawn is greater than the max enemys allowed, then spawn the max enemys that is allowed and substract until its less then maxEnemysAllowed
                if (wave.CanSpawnEnemy())
                {
                    if (wave.amountToSpawn > maxEnemysAllowed)
                    {
                        wave.amountToSpawn -= maxEnemysAllowed;
                        StartCoroutine(SpawnWave(maxEnemysAllowed));
                    }
                    else
                    {
                        int amount = wave.amountToSpawn;
                        wave.amountToSpawn = (wave.enemysAlive % wave.amountToSpawn);
                        amount = amount - wave.amountToSpawn;
                        StartCoroutine(SpawnWave(amount));
                    }
                }
            }
            if (wave.IsFinished())
            {
                wave.NewWave();
            }
        }
        else
        {
            spawnDelayCountdown -= Time.deltaTime;
        }
    }
    public IEnumerator SpawnWave(int amountToSpawn)
    {
        Debug.Log("wave #"+wave.currentWave+", "+ amountToSpawn + ", Spawning enemys...");
        wave.enemysAlive += amountToSpawn;
        for (int i = 0; i < amountToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        yield break;
    }
    private void SpawnEnemy()
    {
        int randomPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[randomPoint].position, Quaternion.identity);
    }
}
