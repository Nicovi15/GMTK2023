using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Cards")]
    [SerializeField]
    List<CardData> levelCards = new List<CardData>();

    [SerializeField]
    int maxRessource = 0;

    [Header("Objectives")]
    public Transform playerStartPosition;
    public FinalTarget finalTarget;

    [Header("Camera settings")]
    public Camera LevelCamera;
    public Color backgroundColor;

    public List<CardData> LevelCards
    {
        get => levelCards;
    }

    public int MaxRessource
    {
        get => maxRessource;
    }

    public void AddCredits(int amount)
    {
        maxRessource += amount;
    }
}
