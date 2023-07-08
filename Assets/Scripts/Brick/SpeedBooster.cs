using UnityEngine;

public class SpeedBooster : MonoBehaviour, IEffector
{
    [SerializeField] [Range(0f, 10f)] float addedSpeed;
    
    public void ApplyEffectOn(in GameJamCharacter player)
    {
        player.IncreaseSpeed(addedSpeed);
    }
}
