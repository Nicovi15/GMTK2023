using UnityEngine;

public class GameJamCharacter : MonoBehaviour
{
    public Vector3 direction = new Vector3(0, 0, 1);
    [Range(0f, 30f)] public float speed = 1f;
    [Range(0f, 30f)] public float maxSpeed = 20f;
    
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Constant movement without ignoring gravity force
        rigidbody.velocity = direction.normalized * speed + new Vector3(0f, rigidbody.velocity.y, 0f);
    }

    public void ChangeDirection(in Vector3 newDirection)
    {
        direction = newDirection;
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
