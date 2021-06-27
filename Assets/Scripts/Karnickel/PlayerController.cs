using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Char {
    public class PlayerController : MonoBehaviour {

        //public LevelController levelController;
        
        //private AttackController _attackController;
        
        [SerializeField] private float jumpForce = 100f;
    
        [SerializeField] private LayerMask groundLayerMask; // A mask determining what is ground to the character
        [SerializeField] private Transform groundCheckTransform; // A position marking where to check if the player is grounded.
        [SerializeField] private float movementSmoothing;
        [SerializeField] private bool hasAirControl;
        [SerializeField] [Range(0f,25f)] private float extraJumpGravity = 0.2f;
        [SerializeField] [Range(0f, 0.5f)] private float jumpDelay = 0f;
        [SerializeField] [Range(0f, 3f)] private float paralyzationDuration;
        [SerializeField] [Range(0f, 200f)] private float paralyzationPushbackForce;
        [SerializeField] [Range(0f, 10f)] private float shroomStunTime = 1f;
    
        private Animator _animator;
        private const float GroundedRadius = .2f;
        private bool _isGrounded;
        private bool _isFacingRight;
        private Vector3 _velocity = Vector3.zero;
        private float _jumpTime = 0;
        private bool _shouldJump = false;
        private bool _isParalyzed = false;
    
        private Rigidbody2D _rigidbody2D;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private float _paralyzeEndTime = 0;


        private float _nextIdleClockAnimation = 0;
        private static readonly int IdleClockTrigger = Animator.StringToHash("IdleClockTrigger");
        private static readonly int IsFacingRight = Animator.StringToHash("isFacingRight");
        private static readonly int DamageTrigger = Animator.StringToHash("DamageTrigger");
        private static readonly int ParalyzeEndTrigger = Animator.StringToHash("ParalyzeEndTrigger");
        private static readonly int StunTrigger = Animator.StringToHash("StunTrigger");

        private EventManager _eventManager;
        private KarnickelFXController _fxController;

        private void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            _nextIdleClockAnimation = Time.time + Random.Range(5f, 10f);
            //_attackController = GetComponent<AttackController>();

            _eventManager = GameObject.FindObjectOfType<EventManager>();
            _fxController = gameObject.GetComponent<KarnickelFXController>();
        }


        private void Update() {
            CheckIdleStuff();
            
            _isGrounded = false;

            Collider2D[] colliders =
                // ReSharper disable once Unity.PreferNonAllocApi
                Physics2D.OverlapCircleAll(groundCheckTransform.position, GroundedRadius, groundLayerMask);
            foreach (var c in colliders) {
                if (c.gameObject != gameObject) {
                    _isGrounded = true;
                }
            }

        }

        private void CheckIdleStuff()
        {
            if (_nextIdleClockAnimation < Time.time)
            {
                _nextIdleClockAnimation = Time.time + Random.Range(5f, 10f);
                _animator.SetTrigger(IdleClockTrigger);
                _fxController.StopTap();
            }
        }

        public void Move(float move, bool jump) {
            if (move != 0)
            {
                _fxController.StopTap();
            } else if (_isGrounded && !_isParalyzed)
            {
                _fxController.StartTap();
            }
            if (_isParalyzed)
            {
                if (_paralyzeEndTime - paralyzationDuration + .5 < Time.time && _isGrounded) {
                    _animator.SetBool(IsGrounded, true);
                }
                
                if (_paralyzeEndTime < Time.time)
                {
                    _isParalyzed = false;
                    _animator.SetTrigger(ParalyzeEndTrigger);
                }

                return;
            }
            
            if (_isGrounded || hasAirControl) {
                Vector3 targetVelocity = new Vector2(move * 100f, _rigidbody2D.velocity.y);
                //smoothing movement
                _rigidbody2D.velocity =
                    Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, movementSmoothing);

                if (move > 0 && !_isFacingRight) {
                    Flip();
                } else if (move < 0 && _isFacingRight) {
                    Flip();
                }
            }
        
            if (_isGrounded && jump) {
                this._jumpTime = Time.time + jumpDelay;
                _animator.SetTrigger(Jump);
                _shouldJump = true;
            }
        
            checkJump();


            if (_isGrounded) {
                _animator.SetBool(IsGrounded, true);
                _animator.SetFloat(Speed, Math.Abs(move));
            } else {
                _animator.SetBool(IsGrounded, false);
            
                //add extra gravity just for jumps, feels better
                Vector3 velocity = _rigidbody2D.velocity;
                velocity.y -= extraJumpGravity * Time.deltaTime;
                _rigidbody2D.velocity = velocity;
            }
        }
    
        private void checkJump() {
            if (_shouldJump && Time.time > _jumpTime) {
                _isGrounded = false;
                _rigidbody2D.AddForce(new Vector2(0f, jumpForce));
                _shouldJump = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Alice")) {
                onAliceTouch(other.gameObject);
            } else if (other.CompareTag("Enemy"))
            {
                onEnemyTouch(other.gameObject);
            } else if (other.CompareTag("Spore"))
            {
                onSporeTouch(other.gameObject);
            }
        }

        private void onSporeTouch(GameObject spore)
        {
            _fxController.Ow();
            _animator.SetTrigger(StunTrigger);
            
            _isParalyzed = true;
            _paralyzeEndTime = Time.time + shroomStunTime;
            _rigidbody2D.velocity = Vector2.zero;
        }

        private void onAliceTouch(GameObject alice)
        {
            Debug.Log("I touched Alice!");
            
            AliceController aliceController = alice.GetComponent<AliceController>();
            if (!aliceController.IsTouched)
            {
                aliceController.Touched();
                _eventManager.OnAliceTouched.Invoke();
            }
        }

        private void onEnemyTouch(GameObject enemy)
        {
            Debug.Log("I touched Enemy!");
            
            _fxController.Ow();
            _animator.SetTrigger(DamageTrigger);
            
            _isParalyzed = true;
            _paralyzeEndTime = Time.time + paralyzationDuration;

            _isGrounded = false;
            _shouldJump = false;
            float x = paralyzationPushbackForce;
            float y = paralyzationPushbackForce * 2f;
            if (_isFacingRight)
            {
                x *= -1;
            }
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(new Vector2(x, y));

            Vector3 dir = (transform.position - enemy.transform.position).normalized;
            
            enemy.GetComponent<EnemyController>().TouchedAlice((int) dir.x);
        }


        private void Flip() {
            _isFacingRight = !_isFacingRight;
            _animator.SetBool(IsFacingRight, _isFacingRight);
        }
    }
}
