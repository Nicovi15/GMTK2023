using UnityEngine;

public class Bumper : MonoBehaviour, IInteractable
{
    [Range(0f, 20f)] public float force;

    public void Interact(in GameJamCharacter player)
    {
        player.Jump(force);
    }
}
