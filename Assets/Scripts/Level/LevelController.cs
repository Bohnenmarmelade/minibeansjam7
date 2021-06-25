using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject alicePrefab;
    
    [SerializeField] private List<GameObject> enemyPrefabs;

    [SerializeField] private int level = 1;
    [SerializeField] private int[] enemiesPerLevelConfig = new int[] {5, 10, 15, 18, 20, 22, 23, 24, 25};

    private List<GameObject> _enemySpawnAreas;
    private List<GameObject> _playerSpawnAreas;
    
    private List<GameObject> _enemies;

    private GameObject player;
    private GameObject alice;
    
    
    void Start()
    {
        _enemySpawnAreas = GameObject.FindGameObjectsWithTag("EnemySpawn").ToList();
        _playerSpawnAreas = GameObject.FindGameObjectsWithTag("PlayerSpawn").ToList();
        
        SpawnAlice();
        SpawnEnemies();
    }

    private void SpawnAlice()
    {
        var spawnAreaIndex = Random.Range(0, _enemySpawnAreas.Count);
        SpawnArea spawnArea = _enemySpawnAreas[Random.Range(0, _enemySpawnAreas.Count)].GetComponent<SpawnArea>();

        alice = spawnArea.Spawn(alicePrefab);
        Debug.Log(alice.GetComponent<Collider2D>().bounds);
    }

    private void SpawnEnemies()
    {
        int enemyCount = enemiesPerLevelConfig[level];

        for (int i = 0; i < enemyCount; i++)
        {
            //find random spawnArea
            SpawnArea spawnArea = _enemySpawnAreas[Random.Range(0, _enemySpawnAreas.Count)].GetComponent<SpawnArea>();

            //find random enemyPrefab
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            //spawn randomEnemyPrefab to randomSpawnArea and add to _enemies
            GameObject spawnedEnemy = spawnArea.Spawn(enemy);
            _enemies.Add(spawnedEnemy);
        }
    }

}
