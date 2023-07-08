using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.

public class BrickCard : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private Image thumbnailImage;
    [SerializeField]
    private TextMeshProUGUI costText;

    [Header("Card Data")]
    [SerializeField]
    private CardData data;

    [Header("Interaction Color")]
    [SerializeField]
    private Color DefaultColor;

    private void Start()
    {
        titleText.text = data.Title;
        thumbnailImage.sprite = data.Thumbnail;
        costText.text = data.Cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointerClick()
    {
        Debug.Log("hey");
        //PlayerController.Instance;
        if (!PlayerController.Instance.IsHolding)
        {
            PlayerController.Instance.HoldCard(data);
        }
    }

}
