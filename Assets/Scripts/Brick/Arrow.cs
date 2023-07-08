using UnityEngine;

// Change player direction based on the forward vector of the current object
public class Arrow : MonoBehaviour, IEffector, IInteractable
{
    [SerializeField] private Vector3 rotationStep = new Vector3(0f, 90f, 0f);

    public void ApplyEffectOn(in GameJamCharacter player)
    {
        player.ChangeDirection(transform.forward);
    }

    public void Interact()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.rotation *= Quaternion.Euler(rotationStep);
    }
}
