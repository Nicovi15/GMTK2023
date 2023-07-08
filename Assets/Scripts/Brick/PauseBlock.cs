using System.Collections;
using UnityEngine;

public class PauseBlock : MonoBehaviour, IInteractable
{
    [SerializeField, Range(0f, 5f)] float pauseTimeInSeconds = 2.0f;

    Coroutine _pauseCoroutine;
    GameJamCharacter _currentPlayer;
    
    public void Interact(in GameJamCharacter player)
    {
        _currentPlayer = player;
        _currentPlayer.Pause();

        _pauseCoroutine = StartCoroutine(ResumePlayer_Coroutine());
    }

    // Break current timer
    public void BreakPauseTimer()
    {
        if (_pauseCoroutine == null)
        {
            Debug.LogError("Break Pause was called, but it was never initialized.");
            return;
        }
        
        StopCoroutine(_pauseCoroutine);
        _currentPlayer.Resume();
    }

    private IEnumerator ResumePlayer_Coroutine()
    {
        yield return new WaitForSeconds(pauseTimeInSeconds);
        
        _currentPlayer.Resume();
    }
}
