using System;
using UnityEngine;
using WSWhitehouse.TagSelector;

public class GameJamCharacter : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField, Range(0f, 30f)] float speed = 1f;
    [SerializeField, Range(0f, 30f)] float maxSpeed = 20f;
    [SerializeField, Range(0f, 700f)] float rotationSpeed = 20f;
    [SerializeField, TagSelector] string groundTag;
    [SerializeField] Animator animator;

    public Action onDeath;

    Transform _transform;
    Rigidbody _rigidbody;
    Vector3 _direction = new(0f, 0f, 1f);

    float _baseSpeed;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        ChangeDirection(_transform.forward);
    }

    private void Start() => _baseSpeed = speed;

    private void Update() => SmoothRotateToDirection();

    private void FixedUpdate() => Move();

    private void OnCollisionEnter(Collision col)
    {
        // Do nothing if the player hit the ground after a jump
        // or normally
        if (col.gameObject.CompareTag(groundTag))
            return;
        
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
        Pause();

        onDeath?.Invoke();
        Debug.Log("Player Is Dead !");
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
}
