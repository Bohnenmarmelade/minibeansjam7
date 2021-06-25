using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;

    [SerializeField] private GameObject _gameObject;

    public void Spawn()
    {
        //calculate random x between left and right boundary
        //spawn _gameObject
        //profit
        var leftPosition = leftBoundary.transform.position;
        var rightPosition = rightBoundary.transform.position;
        
        float x = Random.Range(leftPosition.x, rightPosition.x);
        float y = (leftPosition.y + rightPosition.y) / 2;
        
        Vector3 pos = new Vector2(x, y);

        Instantiate(_gameObject, pos, Quaternion.identity);
    }

    private void Start()
    {
        Spawn();
    }
}
