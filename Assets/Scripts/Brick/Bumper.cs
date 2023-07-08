using UnityEngine;

public class Bumper : MonoBehaviour, IEffector
{
    [SerializeField] [Range(0f, 20f)] float force;

    public void ApplyEffectOn(in GameJamCharacter player)
    {
        player.Jump(force);
    }
}
