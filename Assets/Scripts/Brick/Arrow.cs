using UnityEngine;

// Change player direction based on the forward vector of the current object
public class Arrow : MonoBehaviour, IInteractable
{
    public void Interact(in GameJamCharacter player)
    {
        player.ChangeDirection(transform.forward);
    }
}
