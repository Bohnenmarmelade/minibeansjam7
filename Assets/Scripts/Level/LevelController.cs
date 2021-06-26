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
    [SerializeField] private List<GameObject> hostileShroomPrefabs;

    [SerializeField] private int level = 1;
    [SerializeField] private int[] enemiesPerLevelConfig = new int[] {5, 10, 15, 18, 20, 22, 23, 24, 25};
    [SerializeField] private float levelTime = 60f;

    [SerializeField] private int _hostileShroomAmount = 5;
    

    private List<GameObject> _enemySpawnAreas;
    private List<GameObject> _playerSpawnAreas;
    
    private List<GameObject> _enemies;
    private List<GameObject> _hostileShrooms;
    private List<GameObject> _friendlyShrooms;

    private GameObject player;
    private GameObject alice;

    private float levelEndTime;

    private IndicatorBar _indicatorBar;
    
    void Start()
    {
        _indicatorBar = GameObject.FindObjectOfType<IndicatorBar>();
        EventManager eventManager = EventManager.Instance;
        eventManager.OnAliceTouched.AddListener(OnAliceTouched);
        
        _enemySpawnAreas = GameObject.FindGameObjectsWithTag("EnemySpawn").ToList();
        _playerSpawnAreas = GameObject.FindGameObjectsWithTag("PlayerSpawn").ToList();
        _enemies = new List<GameObject>();
        _hostileShrooms = new List<GameObject>();
        _friendlyShrooms = new List<GameObject>();

        SpawnAlice();
        StartCoroutine(nameof(SpawnEnemies));
        StartCoroutine(nameof(SpawnHostileShrooms));
        
        _indicatorBar.SetMaxTime(levelTime);

        levelEndTime = Time.time + levelTime;
    }

    private void OnDestroy()
    {
        EventManager eventManager = EventManager.Instance;
        eventManager.OnAliceTouched.RemoveListener(OnAliceTouched);
    }

    private void Update()
    {
        _indicatorBar.SetTimeLeft(levelEndTime - Time.time);
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
    }

    private IEnumerator SpawnEnemies()
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

            yield return null;
        }
    }

    private IEnumerator SpawnHostileShrooms()
    {
        for (int i = 0; i < _hostileShroomAmount; i++)
        {
            SpawnArea spawnArea = _enemySpawnAreas[Random.Range(0, _enemySpawnAreas.Count)].GetComponent<SpawnArea>();

            GameObject shroom = hostileShroomPrefabs[Random.Range(0, hostileShroomPrefabs.Count)];

            GameObject spawnedShroom = spawnArea.Spawn(shroom);
            _hostileShrooms.Add(spawnedShroom);

            yield return null;
        }
    }

    private void OnAliceTouched()
    {
        Destroy(alice, 1f);
        level++;
        SpawnAlice();
        StartCoroutine(nameof(SpawnEnemies));
    }

}
