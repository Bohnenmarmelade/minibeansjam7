using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    
    [SerializeField] private LayerMask groundLayerMask; // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheckTransformLeft;
    [SerializeField] private Transform groundCheckTransformRight;
    [SerializeField] private float movementSmoothing;
    [SerializeField] private float pauseTime = 1;
    [SerializeField] private float attackTime = 3;


    private Rigidbody2D _rigidbody2D;
    
    private Vector3 _velocity = Vector3.zero;
    private int _movementDirection;

    private bool _isPause = false;
    private float _pauseEndTime = 0;

    private bool _isAttack = false;
    private float _attackAndTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _movementDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckPause();
        CheckAttack();
    }

    void FixedUpdate()
    {
        if (!_isPause && !_isAttack)
        {
            CheckDirection();
            Walk();
        }
    }

    private void CheckAttack()
    {
        if (_isAttack && _attackAndTime < Time.time)
        {
            _isAttack = false;
        }
    }

    private void CheckPause()
    {
        if (_isPause && _pauseEndTime < Time.time)
        {
            _isPause = false;
        }
    }

    private void SetPause()
    {
        _isPause = true;
        _pauseEndTime = Time.time + pauseTime;
    }

    private void CheckDirection()
    {
        if (_movementDirection < 0)
        {
            // ReSharper disable once Unity.PreferNonAllocApi
            Collider2D[] collidersLeft = Physics2D.OverlapCircleAll(groundCheckTransformLeft.position, .2f, groundLayerMask);
            if (collidersLeft.Length == 0)
            {
                _movementDirection = 1;
                SetPause();
            }
        }
        else
        {
            // ReSharper disable once Unity.PreferNonAllocApi
            Collider2D[] collidersRight = Physics2D.OverlapCircleAll(groundCheckTransformRight.position, .2f, groundLayerMask);
            if (collidersRight.Length == 0)
            {
                _movementDirection = -1;
                SetPause();
            }
        }
        
        
        
    }
    private void Walk()
    {
        Vector3 targetVelocity = new Vector2(walkSpeed * _movementDirection, _rigidbody2D.velocity.y);
        //smoothing movement
        _rigidbody2D.velocity =
            Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, movementSmoothing);
    }

    public void TouchedAlice()
    {
        _isPause = false;
        _isAttack = true;
        _attackAndTime = Time.time + attackTime;
    }
}