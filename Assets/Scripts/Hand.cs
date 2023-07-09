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

    public Vector2 CardDimension;
    public Vector2 Offset;

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
        rectTransform.sizeDelta = new Vector2(cardDatas.Count * (CardDimension.x + Offset.x) + 2 * Offset.x, CardDimension.y + Offset.y);

        float deltaX = CardDimension.x + Offset.x;
        float initialX = cardDatas.Count % 2 == 0 ? -deltaX / 2 * (cardDatas.Count - 1) : -deltaX * (cardDatas.Count / 2);

        if(brickCards.Count > 0)
        {
            foreach(var bc in brickCards)
            {
                Destroy(bc.gameObject);
            }
        }
        brickCards.Clear();

        for (int i = 0; i < cardDatas.Count; i++)
        {
            GameObject newCard = Instantiate(brickCardPrefab);
            newCard.transform.SetParent(transform);
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
