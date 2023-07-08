using UnityEngine;

public class Bumper : MonoBehaviour, IInteractable
{
    [SerializeField] [Range(0f, 20f)] float force;

    public void Interact(in GameJamCharacter player)
    {
        player.Jump(force);
    }
}
