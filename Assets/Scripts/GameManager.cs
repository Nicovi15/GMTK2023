using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    public LevelManager CurrentLevel => currentLevel;

    public GameObject LevelCompleteHUD;

    LevelManager currentLevel;

    [SerializeField]
    GameObject playerPrefab;

    GameJamCharacter currentPlayer;

    int deathNumber;
    float chrono;

    Coroutine chronoRoutine;

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
        currentLevel.finalTarget.OnReachTarget += OnLevelComplete;
    }

    public void StartLevel()
    {
        deathNumber = -1;
        StartCoroutine(StartRun());
    }

    IEnumerator StartRun()
    {
        deathNumber++;
        chrono = 0;
        GameInfos.Instance.UpdateTimeText(chrono);
        GameInfos.Instance.UpdateDeathText(deathNumber);

        PlayerController.Instance.Ressource = currentLevel.MaxRessource;
        Hand.Instance.UpdateCards();

        // Il faudra clean le dictionnaire et supprimer toutes les bricks déjà placées

        if(currentPlayer != null)
        {
            Destroy(currentPlayer.gameObject);
            currentPlayer = null;
        }

        currentPlayer = Instantiate(playerPrefab, currentLevel.playerStartPosition.position, currentLevel.playerStartPosition.rotation).GetComponent<GameJamCharacter>();
        currentPlayer.Pause();
        currentPlayer.OnDeath += Restart;

        yield return new WaitForSeconds(0.1f);

        currentPlayer.Resume();
        chronoRoutine = StartCoroutine(StartChrono());

    }

    public void Restart()
    {
        if (chronoRoutine != null)
        {
            StopCoroutine(chronoRoutine);
            chronoRoutine = null;
        }

        StartCoroutine(StartRun());
    }

    IEnumerator StartChrono()
    {
        chrono = 0;
        while (true)
        {
            chrono += Time.deltaTime;
            GameInfos.Instance.UpdateTimeText(chrono);
            yield return null;
        }
    }

    public void OnLevelComplete()
    {
        StartCoroutine(LevelCompletedRoutine());
    }

    IEnumerator LevelCompletedRoutine()
    {
        if (chronoRoutine != null)
            StopCoroutine(chronoRoutine);

        // Charger prochain niveau en asynchrone
        LevelCompleteHUD.SetActive(true);

        yield return new WaitForSeconds(3.0f);

        Debug.Log("Next level");
    }

}
