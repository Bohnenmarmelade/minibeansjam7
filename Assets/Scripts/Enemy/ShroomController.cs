using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class ShroomController : MonoBehaviour
{

    [SerializeField] private List<Transform> sporeSpawnList;

    [SerializeField] private GameObject sporePrefab;

    [SerializeField] private float spawnTime = 2f;
    
    private float _nextSpawnTime = 0;

    private List<SporeController> _spores= new List<SporeController>();
    private void Start()
    {
        StartCoroutine(nameof(SpawnSpores));
        _nextSpawnTime += spawnTime;
    }

    private void Update()
    {
        if (_nextSpawnTime < Time.time)
        {
            StartCoroutine(nameof(SpawnSpores));
            _nextSpawnTime += spawnTime;
        }
    }

    private void FixedUpdate()
    {
        List<SporeController> toRemove = new List<SporeController>();
        foreach (SporeController sporeController in _spores)
        {
            if (sporeController.EndTime < Time.time)
            {
                toRemove.Add(sporeController);
                sporeController.Die();
            }
        }

        foreach (SporeController sporeController in toRemove)
        {
            _spores.Remove(sporeController);
        }
    }

    private IEnumerator SpawnSpores()
    {
        foreach (Transform t in sporeSpawnList)
        {
            GameObject spore = Instantiate(sporePrefab, t.position, Quaternion.identity);
            Vector3 dir = (spore.transform.position - this.transform.position).normalized;

            SporeController sporeController = spore.GetComponent<SporeController>();
            sporeController.SpawnDirection(dir);
            
            _spores.Add(sporeController);
            
            yield return null;
        }
    }
}
