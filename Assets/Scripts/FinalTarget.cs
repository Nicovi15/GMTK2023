using UnityEngine;

public class FinalTarget : MonoBehaviour
{
    public System.Action OnReachTarget;

    private void OnTriggerEnter(Collider col)
    {
        if (!col.TryGetComponent(out GameJamCharacter player))
            return;
        
        player.Pause();
        OnReachTarget?.Invoke();
        Debug.Log("The player has reached the end line !");
    }
}
