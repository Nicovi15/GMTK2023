using UnityEngine;

// Change player direction based on the forward vector of the current object
public class ChangeDirectionBrick : MonoBehaviour, IEffector
{
    public void ApplyEffectOn(in GameJamCharacter player)
    {
        player.ChangeDirection(transform.forward);
    }
}
