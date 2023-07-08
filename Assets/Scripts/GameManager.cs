using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    public LevelManager CurrentLevel => currentLevel;

    LevelManager currentLevel;

    [SerializeField]
    GameObject playerPrefab;

    GameJamCharacter currentPlayer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        LoadLevel();
    }

    void LoadLevel()
    {
        currentLevel = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        Hand.Instance.Initialize(currentLevel.LevelCards);
    }

    public void StartLevel()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(3.0f);

        StartCoroutine(StartRun());
    }

    IEnumerator StartRun()
    {
        PlayerController.Instance.Ressource = currentLevel.MaxRessource;

        if(currentPlayer != null)
        {
            Destroy(currentPlayer.gameObject);
            currentPlayer = null;
        }

        currentPlayer = Instantiate(playerPrefab, currentLevel.playerStartPosition.position, currentLevel.playerStartPosition.rotation).GetComponent<GameJamCharacter>();
        currentPlayer.Pause();
        currentPlayer.OnDeath += Restart;

        yield return new WaitForSeconds(1.0f);

        currentPlayer.Resume();

    }

    public void Restart()
    {
        StartCoroutine(StartRun());
    }

}
