using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject alicePrefab;
    
    [SerializeField] private List<GameObject> enemyPrefabs;

    [SerializeField] private int level = 1;
    [SerializeField] private int[] enemiesPerLevelConfig = new int[] {5, 10, 15, 18, 20, 22, 23, 24, 25};
    [SerializeField] private float levelTime = 30000f;

    private List<GameObject> _enemySpawnAreas;
    private List<GameObject> _playerSpawnAreas;
    
    private List<GameObject> _enemies;

    private GameObject player;
    private GameObject alice;

    private float levelEndTime;
    
    
    void Start()
    {
        EventManager eventManager = EventManager.Instance;
        eventManager.OnAliceTouched.AddListener(OnAliceTouched);
        
        _enemySpawnAreas = GameObject.FindGameObjectsWithTag("EnemySpawn").ToList();
        _playerSpawnAreas = GameObject.FindGameObjectsWithTag("PlayerSpawn").ToList();
        _enemies = new List<GameObject>();

        SpawnAlice();
        SpawnEnemies();

        levelEndTime = Time.time + levelTime;
    }

    private void OnDestroy()
    {
        EventManager eventManager = EventManager.Instance;
        eventManager.OnAliceTouched.RemoveListener(OnAliceTouched);
    }

    private void Update()
    {
        CheckLevelTimer();
    }

    private void CheckLevelTimer()
    {
        
        if (levelEndTime < Time.time)
        {
            //game over
            Debug.Log("Game Over!");
            EventManager.Instance.OnGameOver.Invoke();
        } 
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
        int enemyCount = enemiesPerLevelConfig[level] - _enemies.Count;
        
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

    private void OnAliceTouched()
    {
        Destroy(alice, 0);
        level++;
        SpawnAlice();
        SpawnEnemies();
    }

}
