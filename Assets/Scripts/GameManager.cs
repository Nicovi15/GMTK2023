using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    LevelManager currentLevel;

    void Start()
    {
        StartNewLevel();
    }

    void StartNewLevel()
    {
        currentLevel = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        PlayerController.Instance.Ressource = currentLevel.MaxRessource;
        Hand.Instance.Initialize(currentLevel.LevelCards);
    }

}
