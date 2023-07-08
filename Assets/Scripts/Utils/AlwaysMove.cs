using UnityEngine;

public class AlwaysMove : MonoBehaviour
{
    [SerializeField, Range(0f, 2f)] float speed = 0.5f;

    Transform _transform;
    Vector3 _originalPosition;

    private void Awake()
    {
        _transform = transform;
        _originalPosition = _transform.position;
    }

    private void Update()
    {
        var upMovement = Vector3.up * (Mathf.Sin(Time.time) * speed);
        _transform.position = _originalPosition + upMovement;
    }
}
