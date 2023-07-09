using UnityEngine;

public class RagdollController : MonoBehaviour
{
    Collider[] _colliders;
    Rigidbody[] _rigidbodies;
    CharacterJoint[] _characterJoints;

    public void Initialize()
    {
        _colliders = GetComponentsInChildren<Collider>();
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _characterJoints = GetComponentsInChildren<CharacterJoint>();
    }

    public void Enable() => ChangeRagdollState(true);

    public void Disable() => ChangeRagdollState(false);

    private void ChangeRagdollState(bool state)
    {
        if (_colliders != null)
        {
            foreach (var currentCollider in _colliders)
                currentCollider.enabled = state;
        }

        if (_rigidbodies != null)
        {
            foreach (var currentRigidbody in _rigidbodies)
                currentRigidbody.isKinematic = !state;
        }
    }
}
