using UnityEngine;

public class SpeedBoost : MonoBehaviour, IInteractable
{
    [Range(0f, 10f)] public float addedSpeed;
    
    public void Interact(in GameJamCharacter player)
    {
        player.IncreaseSpeed(addedSpeed);
    }
}
