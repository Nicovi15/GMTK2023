using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelDescription
{
    public string Name;
    public Color Color;
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    public LevelManager CurrentLevel => currentLevel;

    public LevelComplete LevelCompleteHUD;

    public Menu menu;

    LevelManager currentLevel;

    [SerializeField]
    GameObject playerPrefab;

    GameJamCharacter currentPlayer;

    int deathNumber;
    float chrono;

    Coroutine chronoRoutine;

    public List<LevelDescription> Levels = new List<LevelDescription>();

    public int levelIndex = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        LevelCompleteHUD.OnEndLevelComplete += UnloadPreviousLevel;
    }

    void Start()
    {
        //StartCoroutine(LoadLevel());
    }

    public void StartLoadLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        menu.HidePanel.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Levels[levelIndex].Name, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        currentLevel = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        Hand.Instance.Initialize(currentLevel.LevelCards);
        currentLevel.finalTarget.OnReachTarget += OnLevelComplete;
        menu.HidePanel.SetActive(false);
        StartLevel();
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
        PlayerController.Instance.ClearGrid();
        Hand.Instance.UpdateCards();

        // Il faudra clean le dictionnaire et supprimer toutes les bricks d�j� plac�es

        if(currentPlayer != null)
        {
            Destroy(currentPlayer.gameObject);
            currentPlayer = null;
        }

        currentPlayer = Instantiate(playerPrefab, currentLevel.playerStartPosition.position, currentLevel.playerStartPosition.rotation).GetComponent<GameJamCharacter>();
        currentPlayer.Pause();
        currentPlayer.onDeath += Restart;

        yield return new WaitForSeconds(0.1f);

        currentPlayer.Resume();
        chronoRoutine = StartCoroutine(StartChrono());

    }

    void UnloadPreviousLevel()
    {
        if (currentPlayer != null)
        {
            Destroy(currentPlayer.gameObject);
            currentPlayer = null;
        }
        StartCoroutine(UnloadPreviousLevelRoutine());
    }

    IEnumerator UnloadPreviousLevelRoutine()
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(Levels[levelIndex].Name);
        while (!asyncUnload.isDone)
        {
            yield return null;
        }

        levelIndex++;
        if(levelIndex == Levels.Count)
        {
            Debug.Log("Fin du jeu");
        }
        else
        {
            StartCoroutine(LoadLevel());
        }

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
        if (chronoRoutine != null)
            StopCoroutine(chronoRoutine);

        LevelCompleteHUD.gameObject.SetActive(true);
        StartCoroutine(LevelCompleteHUD.ShowLevelComplete((int)chrono, deathNumber, new Color()));

    }


}
