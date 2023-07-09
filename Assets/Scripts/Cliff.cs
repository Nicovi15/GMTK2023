using UnityEngine;

// Kill player when he enter in a cliff
public class Cliff : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out GameJamCharacter jamCharacter))
        {
            jamCharacter.Death();
        }
    }
}