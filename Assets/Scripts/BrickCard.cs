using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BrickCard : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private Image background;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private Image thumbnailImage;
    [SerializeField]
    private TextMeshProUGUI costText;

    [Header("Card Data")]
    [SerializeField]
    private CardData data;

    public CardData Data
    {
        get => data;
    }

    public bool CanBeUse
    {
        get => canBeUse;
    }

    [Header("Interaction Color")]
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color highLightColor;
    [SerializeField]
    private Color selectColor;
    [SerializeField]
    private Color disableColor;

    private bool canBeUse = true;
    private bool isSelected = false;


    private void Start()
    {

    }

    public void InitializeCard(CardData newData)
    {
        data = newData;
        titleText.text = data.Title;
        thumbnailImage.sprite = data.Thumbnail;
        costText.text = data.Cost.ToString();
    }

    public void HoldCard()
    {
        if (!PlayerController.Instance.IsHolding && canBeUse)
        {
            PlayerController.Instance.HoldCard(this);
            SelectCard(true);
        }
    }

    public void SelectCard(bool shoudBeSelected)
    {
        if (shoudBeSelected)
        {
            background.color = selectColor;
            this.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
        else
        {
            background.color = defaultColor;
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        isSelected = shoudBeSelected;
    }

    public void HighlightCard(bool shouldBeHighlight)
    {
        if (isSelected || !canBeUse || PlayerController.Instance.IsHolding)
            return;

        if (shouldBeHighlight)
        {
            background.color = highLightColor;
            this.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
        else 
        {
            background.color = defaultColor;
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    public void DisableCard(bool shouldBeDisabled)
    {
        canBeUse = !shouldBeDisabled;

        if (shouldBeDisabled)
        {
            background.color = disableColor;
        }
        else
        {
            background.color = defaultColor;
        }
    }

}
