using UnityEngine;

// Trigger all IInteractable attached to the current game object
public class OnTriggerInteractable : MonoBehaviour
{
    // Split activation / collision management and real interaction
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out GameJamCharacter player))
        {
            var interactables = GetComponents<IInteractable>();
            foreach (var currentInteractable in interactables)
            {
                currentInteractable.Interact(player);
            }
        }
    }
}
