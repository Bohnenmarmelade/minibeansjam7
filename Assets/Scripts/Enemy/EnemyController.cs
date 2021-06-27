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

    private bool _isFacingRight = true;

    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int AttackTrigger = Animator.StringToHash("AttackTrigger");
    private SpriteRenderer _spriteRenderer;

    private EnemyFXController _fxController;

    // Start is called before the first frame update
    void Start()
    {
        _fxController = gameObject.GetComponent<EnemyFXController>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _movementDirection = Random.value < .5f ? -1 : 1;
        if (_movementDirection < 0)
        {
            Flip();
        }
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        _animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        CheckPause();
        CheckAttack();
        
        if (!_isPause && !_isAttack)
        {
            Walk();
            CheckDirection();

            if (_movementDirection < 0 && _isFacingRight)
            {
                Flip();
            } else if (_movementDirection > 0 && !_isFacingRight)
            {
                Flip();
            }
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
        _fxController.StopWalk();
        _animator.SetFloat(Speed, 0f);
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
        _fxController.StartWalk();
        _animator.SetFloat(Speed, walkSpeed);
        Vector3 targetVelocity = new Vector2(walkSpeed * _movementDirection, _rigidbody2D.velocity.y);
        //smoothing movement
        _rigidbody2D.velocity =
            Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, movementSmoothing);
    }

    public void TouchedAlice(int fromDirX)
    {
        _animator.SetTrigger(AttackTrigger);
        _animator.SetFloat(Speed, 0);
        _isPause = false;
        _isAttack = true;
        _attackAndTime = Time.time + attackTime;
        
        _fxController.Attack();
    }
    
    private void Flip() {
        _isFacingRight = !_isFacingRight;

        _spriteRenderer.flipX = !_isFacingRight;
        
        
    }
}
