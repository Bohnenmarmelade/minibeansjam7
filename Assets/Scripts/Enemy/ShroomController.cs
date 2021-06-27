using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShroomController : MonoBehaviour
{

    [SerializeField] private List<Transform> sporeSpawnList;

    [SerializeField] private GameObject sporePrefab;

    [SerializeField] private float spawnTime = 2f;

    [SerializeField] private float animationOffset = .2f;
    
    private float _nextSpawnTime = 0;
    private float _nextSpawnAnimationTime = 0;
    private Animator _animator;

    private List<SporeController> _spores= new List<SporeController>();
    private static readonly int ExplodeTrigger = Animator.StringToHash("ExplodeTrigger");

    private ShroomFXController _fxController;
    private void Start()
    {
        _nextSpawnTime += Random.Range(3f, 8f);
        _nextSpawnAnimationTime = _nextSpawnTime - animationOffset;

        _animator = GetComponent<Animator>();
        _fxController = gameObject.GetComponent<ShroomFXController>();
    }

    private void Update()
    {
        if (_nextSpawnAnimationTime < Time.time)
        {
            _nextSpawnAnimationTime += spawnTime;
            _animator.SetTrigger(ExplodeTrigger);
            _fxController.Fart();
        }
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
