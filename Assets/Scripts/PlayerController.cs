using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private bool isHolding;

    private GameObject projectionPrefab;
    private GameObject brickPrefab;
    Ray ray;
    Plane plane = new Plane(Vector3.up, 0);

    private GameObject currentProjection;

    public LayerMask GridLayer;

    public bool IsHolding
    {
        get => isHolding;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHolding)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData, 1000, GridLayer))
            {
                Debug.Log(hitData.point);
                if (currentProjection == null)
                    currentProjection = Instantiate(projectionPrefab);

                currentProjection.transform.position = Grid.Instance.GetSnapPosition(hitData.point);
            }
            else
            {
                if (currentProjection != null)
                    Destroy(currentProjection);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("up");
                if(currentProjection != null)
                {
                    GameObject newBrick = Instantiate(brickPrefab);
                    newBrick.transform.position = currentProjection.transform.position;
                    newBrick.transform.rotation = currentProjection.transform.rotation;
                    newBrick.transform.localScale = currentProjection.transform.localScale;

                    Destroy(currentProjection);
                }

                isHolding = false;
            }
        }
    }

    public void HoldCard(CardData data)
    {
        isHolding = true;
        projectionPrefab = data.ProjectionPrefab;
        brickPrefab = data.BrickPrefab;
    }

}
