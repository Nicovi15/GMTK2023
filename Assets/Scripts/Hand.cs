using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private static Hand instance;
    public static Hand Instance
    {
        get => instance;
    }

    [SerializeField]
    GameObject brickCardPrefab;

    List<BrickCard> brickCards = new List<BrickCard>();

    RectTransform rectTransform;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        rectTransform = GetComponent<RectTransform>();
    }
    public void Initialize(List<CardData> cardDatas)
    {
        rectTransform.sizeDelta = new Vector2(cardDatas.Count * (150.0f + 10.0f) + 20.0f, 250);

        float deltaX = 150.0f + 10.0f;
        float initialX = cardDatas.Count % 2 == 0 ? -deltaX / 2 * (cardDatas.Count - 1) : -deltaX * (cardDatas.Count / 2);

        brickCards.Clear();

        for (int i = 0; i < cardDatas.Count; i++)
        {
            GameObject newCard = Instantiate(brickCardPrefab);
            newCard.transform.parent = transform;
            RectTransform rt = newCard.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(initialX, 0);
            initialX += deltaX;

            brickCards.Add(newCard.GetComponent<BrickCard>());
            brickCards[i].InitializeCard(cardDatas[i]);
        }

        UpdateCards();
    }

    public void UpdateCards()
    {
        foreach(BrickCard bc in brickCards)
        {
            if(bc.CanBeUse && bc.Data.Cost > PlayerController.Instance.Ressource)
            {
                bc.DisableCard(true);
                continue;
            }

            if (!bc.CanBeUse && bc.Data.Cost <= PlayerController.Instance.Ressource)
            {
                bc.DisableCard(false);
                continue;
            }
        }
    }
}
