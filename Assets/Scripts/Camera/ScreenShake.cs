using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] [Range(1f, 10f)] private float screenShakeForce;
    [SerializeField] [Range(0f, 1f)] private float screenShakeDuration;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private bool _isShaking = false;
    private float _shakeEndTime = 0f;

    private void Start()
    {
        _cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
    }

    private void CheckShaking()
    {
        if (_isShaking && _shakeEndTime < Time.time)
        {
            _isShaking = false;
            //_cinemachineVirtualCamera.
        }
        
    }
}
