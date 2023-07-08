using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    List<CardData> levelCards = new List<CardData>();

    [SerializeField]
    int maxRessource = 0;

    public List<CardData> LevelCards
    {
        get => levelCards;
    }

    public int MaxRessource
    {
        get => maxRessource;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
