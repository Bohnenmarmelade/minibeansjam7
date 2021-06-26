using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SporeController : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private float lifeTime = 2f;
    
    private Vector3 _flyDirection = Vector3.up;

    private float _endTime;

    public float EndTime => _endTime;

    public void SpawnDirection(Vector2 dir)
    {
        _flyDirection = dir;
        _endTime = Time.time + lifeTime;
    }

    private void Update()
    {
        
        Vector3 pos = transform.position;
        Vector3 deltaNextPos = _flyDirection * (speed * Time.deltaTime);

        pos += deltaNextPos;

        transform.position = pos;
    }

    public void Die()
    {
        Destroy(gameObject, 0f);
    }
}
