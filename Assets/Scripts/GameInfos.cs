using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfos : MonoBehaviour
{
    private static GameInfos instance;
    public static GameInfos Instance => instance;

    [SerializeField]
    TextMeshProUGUI TimeText;
    [SerializeField]
    TextMeshProUGUI DeathText;
    [SerializeField]
    TextMeshProUGUI RessourceText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void UpdateTimeText(float time)
    {
        TimeText.text = ((int) time).ToString();
    }

    public void UpdateDeathText(int death)
    {
        DeathText.text = death.ToString();
    }

    public void UpdateRessourceText(int ressource)
    {
        RessourceText.text = ressource.ToString();
    }
}
