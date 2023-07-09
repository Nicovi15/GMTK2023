using System;
using System.Collections;
using UnityEngine;
using WSWhitehouse.TagSelector;

public class GameJamCharacter : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField, Range(0f, 30f)] float speed = 1f;
    [SerializeField, Range(0f, 30f)] float maxSpeed = 20f;
    [SerializeField, Range(0f, 700f)] float rotationSpeed = 20f;
    [SerializeField] Animator animator;

    [Header("Jump")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform footTransform;
    [SerializeField, Range(0f, 1f)] float groundCheckRadius = 0.1f;
    
    [Header("Death")]
    [SerializeField, TagSelector] string groundTag = "Ground";
    [SerializeField] RagdollController ragdollController;
    [SerializeField, Range(0f, 3f)] float deathTimeInSeconds = 2.0f;
    
    public Action onDeath;

    Transform _transform;
    Rigidbody _rigidbody;
    Collider _collider;
    Vector3 _direction = new(0f, 0f, 1f);

    float _baseSpeed;
    bool _bGrounded;
    bool _bDead;
    
    private static readonly int BJump = Animator.StringToHash("bJump");

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _transform = GetComponent<Transform>();
        ChangeDirection(_transform.forward);
    }

    private void Start()
    {
        _baseSpeed = speed;
        ragdollController.Initialize();
        ragdollController.Disable();
        _collider.enabled = true;
    }

    private void Update()
    {
        // Change animation only when the player takes off or land
        if (_bGrounded != IsCurrentlyGrounded())
        {
            _bGrounded = !_bGrounded;
            
            // Jump only when the player isn't grounded
            animator.SetBool(BJump, !_bGrounded);
        }
        
        SmoothRotateToDirection();
    }

    private void FixedUpdate() => Move();

    private void OnCollisionEnter(Collision col)
    {
        // Do nothing if the player hit the ground after a jump
        // or normally
        if (col.gameObject.CompareTag(groundTag))
            return;
        
        Debug.Log("Player hit by " + col.gameObject.name);
        
        Death();
    }
    
    public void ChangeDirection(in Vector3 newDirection)
    {
        _direction = newDirection;
    }

    public void IncreaseSpeed(float addedSpeed)
    {
        speed += addedSpeed;
        speed = Mathf.Clamp(speed, 0f, maxSpeed);

        // Change animation speed on increase speed
        animator.speed = speed / _baseSpeed;
    }

    public void Jump(float jumpForce)
    {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void Pause()
    {
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
        enabled = false;
        animator.enabled = false;
    }

    public void Resume()
    {
        enabled = true;
        animator.enabled = true;
    }

    public void Death()
    {
        if (_bDead)
            return;

        Pause();

        _bDead = true;

        // Disable main collider / rigidbody to be replace by ragdoll physics
        _rigidbody.useGravity = false;
        _collider.enabled = false;
        animator.enabled = false;
        ragdollController.Enable();

        StartCoroutine(InvokeOnDeath_Coroutine());
    }

    private void Move()
    {
        // Constant movement without ignoring gravity force
        _rigidbody.velocity = _direction.normalized * speed + Vector3.up * _rigidbody.velocity.y;
    }

    // Rotate the player to follow the current direction
    private void SmoothRotateToDirection()
    {
        // Convert direction to rotation
        Quaternion directionRotation = Quaternion.LookRotation(_direction, Vector3.up);
        
        // Smooth rotation with an interpolation value
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, directionRotation, rotationSpeed * Time.deltaTime);
    }

    private bool IsCurrentlyGrounded()
    {
        return Physics.CheckSphere(footTransform.position, groundCheckRadius, groundLayer);
    }

    private IEnumerator InvokeOnDeath_Coroutine()
    {
        yield return new WaitForSeconds(deathTimeInSeconds);

        onDeath?.Invoke();
        Debug.Log("Player is dead !");
    }
}
