using System;
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
        set 
        {
           ressource = value;
            GameInfos.Instance.UpdateRessourceText(ressource);
        }
    }
    
    [Header("Layermasks")]
    [SerializeField]
    private LayerMask GridLayer;
    [SerializeField]
    private LayerMask InteractableLayer;
    private bool isHolding;
    public int ressource = 0;
    private BrickCard selectedCard;
    private GameObject currentProjection;

    public Action OnRessourceUpdate;

    Dictionary<Vector2Int, GameObject> bricksOnGrid = new Dictionary<Vector2Int, GameObject>();

    private Vector2Int currentPosInGrid;

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
                currentPosInGrid = Grid.Instance.GetGridCoordinate(hitData.point);
                if (!bricksOnGrid.ContainsKey(currentPosInGrid))
                {
                    if (currentProjection == null)
                        currentProjection = Instantiate(selectedCard.Data.ProjectionPrefab);

                    currentProjection.transform.position = Grid.Instance.GetSnapPosition(hitData.point);
                }
                else
                {
                    if (currentProjection != null)
                    {
                        Destroy(currentProjection);
                        currentProjection = null;
                    }
                }
            }
            else
            {
                if (currentProjection != null)
                    Destroy(currentProjection);
            }

            if (Input.GetMouseButtonUp(0))
            {
                selectedCard.SelectCard(false);

                if (currentProjection != null && !bricksOnGrid.ContainsKey(currentPosInGrid))
                {
                    GameObject newBrick = Instantiate(selectedCard.Data.BrickPrefab);
                    newBrick.transform.position = currentProjection.transform.position;
                    newBrick.transform.rotation = currentProjection.transform.rotation;
                    newBrick.transform.localScale = currentProjection.transform.localScale;
                    bricksOnGrid.Add(currentPosInGrid, newBrick);

                    Destroy(currentProjection);
                    Ressource -= selectedCard.Data.Cost;
                    Hand.Instance.UpdateCards();
                }
                
                selectedCard = null;
                isHolding = false;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitData;
                if (Physics.Raycast(ray, out hitData, 1000, InteractableLayer))
                {
                    hitData.collider.gameObject.GetComponent<IInteractable>().Interact();
                }
            }
        }
    }

    public void HoldCard(BrickCard card)
    {
        isHolding = true;
        selectedCard = card;
    }

    public void ClearGrid()
    {
        foreach(var brick in bricksOnGrid)
        {
            Destroy(brick.Value);
        }

        bricksOnGrid.Clear();
    }

}
