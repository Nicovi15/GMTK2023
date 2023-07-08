using System.Collections;
using UnityEngine;

public class PauseBlock : MonoBehaviour, IEffector, IInteractable
{
    [SerializeField, Range(0f, 5f)] float pauseTimeInSeconds = 2.0f;

    Coroutine _pauseCoroutine;
    GameJamCharacter _currentPlayer;
    
    public void ApplyEffectOn(in GameJamCharacter player)
    {
        _currentPlayer = player;
        _currentPlayer.Pause();

        _pauseCoroutine = StartCoroutine(ResumePlayer_Coroutine());
    }

    public void Interact()
    {
        BreakTimer();
    }

    // Break current timer
    public void BreakTimer()
    {
        if (_pauseCoroutine == null)
        {
            Debug.LogError("Break Pause was called, but it was never initialized.");
            return;
        }
        
        StopCoroutine(_pauseCoroutine);
        ResumePlayer();
    }

    private IEnumerator ResumePlayer_Coroutine()
    {
        yield return new WaitForSeconds(pauseTimeInSeconds);
        
        ResumePlayer();
    }
    
    private void ResumePlayer()
    {
        _currentPlayer.Resume();
        Destroy(gameObject);
    }
}
