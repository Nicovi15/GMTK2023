using UnityEngine;

public class AlwaysRotate : MonoBehaviour
{
    [SerializeField, Range(0f, 180f)] float speed = 120.0f;

    Transform _transform;

    private void Awake() => _transform = transform;

    private void Update() => _transform.Rotate(Vector3.up * (speed * Time.deltaTime));
}
