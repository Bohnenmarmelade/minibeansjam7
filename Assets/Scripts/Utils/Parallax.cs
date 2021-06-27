using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Camera _camera;

    [SerializeField]
    [Range(0f, 10f)] private float parallaxAmount = 3f;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update() {
        Vector3 pos = transform.position;
        pos.x = _camera.transform.position.x / parallaxAmount;
        transform.position = pos;
    }
}
