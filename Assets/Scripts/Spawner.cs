using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

public Wave[] waves;
    public Transform enemy;

    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesReaminingAlive;
    float nextSpawnTime;

    void Start(){
        NextWave();
    }

    void Update(){

        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime){

            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            Transform spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Transform;
            //spawnedEnemy.OnDeath += OnEnemyDeath;

        }

    }

    void OnEnemyDeath()
    {
        enemiesReaminingAlive--;

        if(enemiesReaminingAlive == 0){
            NextWave();
        }
    }

    void NextWave(){
        currentWaveNumber++;

        if (currentWaveNumber - 1 < waves.Length){

            currentWave = waves[currentWaveNumber - 1];
            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesReaminingAlive = enemiesRemainingToSpawn;

        }

    }

    [System.Serializable]
	public class Wave{
        public int enemyCount;
        public float timeBetweenSpawns;

    }

}
