using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "CardData", order = 1)]
public class CardData : ScriptableObject
{
    public string Title;
    public int Cost;
    public Sprite Thumbnail;
    public GameObject ProjectionPrefab;
    public GameObject BrickPrefab;

}
