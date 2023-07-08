using UnityEngine;

// Trigger all IInteractable attached to the current game object
public class OnTriggerInteractable : MonoBehaviour
{
    // Split activation / collision management and real interaction
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out GameJamCharacter player))
        {
            var effectors = GetComponents<IEffector>();
            foreach (var effector in effectors)
            {
                effector.ApplyEffectOn(player);
            }
        }
    }
}
