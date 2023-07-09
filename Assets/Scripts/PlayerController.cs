using System;
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
                {
                    currentProjection = Instantiate(selectedCard.Data.BrickPrefab);
                    var projectionEffect = currentProjection.GetComponent<IProjectionEffect>();
                    if (projectionEffect != null)
                    {
                        projectionEffect.ShowHighlight();
                    }

                    // Disable collider on the projection object to keep the player unaffected
                    var projectionCollider = currentProjection.GetComponent<Collider>();
                    projectionCollider.enabled = false;
                }

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
                    var interactables = hitData.collider.gameObject.GetComponents<IInteractable>();
                    foreach (var currentInteractable in interactables)
                    {
                        currentInteractable.Interact();
                    }
                }
            }
        }
    }

    public void HoldCard(BrickCard card)
    {
        isHolding = true;
        selectedCard = card;
    }

}
