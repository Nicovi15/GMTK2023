using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    List<CardData> levelCards = new List<CardData>();

    [SerializeField]
    int maxRessource = 0;

    public Transform playerStartPosition;

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
