using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance;

    public float cellSize = 5.0f;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        // Debug.Log(GetGridCoordinate(new Vector3(-4.0f, -1.0f, -1.0f)));
        // Debug.Log(GetGridCoordinate(new Vector3(-4.0f, 1.0f, 6.0f)));
    }

    public Vector2Int GetGridCoordinate(in Vector3 position)
    {
        return new Vector2Int((int)(position.x / cellSize), (int)(position.z / cellSize));
    }

    public Vector3 GetSnapPosition(in Vector3 rawPosition)
    {
        var gridCoordinate = GetGridCoordinate(rawPosition);

        // Snap position to the center of grid
        var halfOffset = GetHalfCellOffset();
        
        // Signed in the same direction than the grid coordinate
        var signedHalfOffset = new Vector3(halfOffset.x * Mathf.Sign(gridCoordinate.x), 
                                            0f,
                                            halfOffset.z * Mathf.Sign(gridCoordinate.y));
        
        return new Vector3(gridCoordinate.x, rawPosition.y, gridCoordinate.y) * cellSize + signedHalfOffset;
    }

    public Vector3 GetHalfCellOffset()
    {
        return new Vector3(cellSize, 0f, cellSize) / 2.0f;
    }
}
