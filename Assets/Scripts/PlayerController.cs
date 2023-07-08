using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get => instance;
    }

    public bool IsHolding
    {
        get => isHolding;
    }

    public int Ressource
    {
        get => ressource;
        set => ressource = value; 
    }
    
    [Header("Grid Layermask")]
    [SerializeField]
    private LayerMask GridLayer;
    private bool isHolding;
    public int ressource = 0;
    private BrickCard selectedCard;
    private GameObject currentProjection;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        if (isHolding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData, 1000, GridLayer))
            {
                if (currentProjection == null)
                    currentProjection = Instantiate(selectedCard.Data.ProjectionPrefab);

                currentProjection.transform.position = Grid.Instance.GetSnapPosition(hitData.point);
            }
            else
            {
                if (currentProjection != null)
                    Destroy(currentProjection);
            }

            if (Input.GetMouseButtonUp(0))
            {
                selectedCard.SelectCard(false);

                if (currentProjection != null)
                {
                    GameObject newBrick = Instantiate(selectedCard.Data.BrickPrefab);
                    newBrick.transform.position = currentProjection.transform.position;
                    newBrick.transform.rotation = currentProjection.transform.rotation;
                    newBrick.transform.localScale = currentProjection.transform.localScale;

                    Destroy(currentProjection);
                    ressource -= selectedCard.Data.Cost;
                    Hand.Instance.UpdateCards();
                }
                
                selectedCard = null;
                isHolding = false;
            }
        }
    }

    public void HoldCard(BrickCard card)
    {
        isHolding = true;
        selectedCard = card;
    }

}
