using UnityEngine;

public class RagdollController : MonoBehaviour
{
    Collider[] _colliders;
    Rigidbody[] _rigidbodies;
    CharacterJoint[] _characterJoints;

    private void Awake()
    {
        _colliders = GetComponentsInChildren<Collider>();
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _characterJoints = GetComponentsInChildren<CharacterJoint>();
    }

    public void Enable() => ChangeRagdollState(true);

    public void Disable() => ChangeRagdollState(false);

    private void ChangeRagdollState(bool state)
    {
        foreach (var currentCollider in _colliders)
            currentCollider.enabled = state;
        
        foreach (var currentRigidbody in _rigidbodies)
            currentRigidbody.isKinematic = !state;
    }
}
