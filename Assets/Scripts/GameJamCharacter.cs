using UnityEngine;

public class GameJamCharacter : MonoBehaviour
{
    [SerializeField] Vector3 baseDirection = new(0, 0, 1);
    [SerializeField] [Range(0f, 30f)] float speed = 1f;
    [SerializeField] [Range(0f, 30f)] float maxSpeed = 20f;
    
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Constant movement without ignoring gravity force
        rigidbody.velocity = baseDirection.normalized * speed + new Vector3(0f, rigidbody.velocity.y, 0f);
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
        rigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
    }
}
