using UnityEngine;

public class SpeedBooster : MonoBehaviour, IInteractable
{
    [SerializeField] [Range(0f, 10f)] float addedSpeed;
    
    public void Interact(in GameJamCharacter player)
    {
        player.IncreaseSpeed(addedSpeed);
    }
}
