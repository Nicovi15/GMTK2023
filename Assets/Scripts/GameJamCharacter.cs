using System;
using UnityEngine;
using WSWhitehouse.TagSelector;

public class GameJamCharacter : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Vector3 baseDirection = new(0, 0, 1);
    [SerializeField] [Range(0f, 30f)] float speed = 1f;
    [SerializeField] [Range(0f, 30f)] float maxSpeed = 20f;
    [SerializeField, TagSelector] private string groundTag;
    
    public Action onDeath;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        ChangeDirection(transform.forward);
    }

    private void FixedUpdate()
    {
        // Constant movement without ignoring gravity force
        _rigidbody.velocity = baseDirection.normalized * speed + Vector3.up * _rigidbody.velocity.y;
    }

    public void ChangeDirection(in Vector3 newDirection)
    {
        baseDirection = newDirection;
    }

    public void IncreaseSpeed(float addedSpeed)
    {
        speed += addedSpeed;
        speed = Mathf.Clamp(speed, 0f, maxSpeed);
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
    }

    public void Resume()
    {
        enabled = true;
    }

    private void OnCollisionEnter(Collision col)
    {
        // Do nothing if the player hit the ground after a jump
        // or normally
        if (col.gameObject.CompareTag(groundTag))
            return;
        
        Death();
    }

    public void Death()
    {
        Pause();

        onDeath?.Invoke();
        Debug.Log("Player Is Dead !");
    }
}
