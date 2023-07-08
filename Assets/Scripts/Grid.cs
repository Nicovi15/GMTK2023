using System;
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
        // Debug.Log(GetGridCoordinate(new Vector3(-1.0f, -1.0f, -1.0f)));
    }

    public Vector2Int GetGridCoordinate(in Vector3 position)
    {
        return new Vector2Int((int)(position.x / cellSize), (int)(position.z / cellSize));
    }

    public Vector3 GetSnapPosition(in Vector3 rawPosition)
    {
        var gridCoordinate = GetGridCoordinate(rawPosition);

        // Snap position to the center of grid
        Vector3 halfCellOffset = new Vector3(cellSize, 0, cellSize) / 2.0f;
        
        return new Vector3(gridCoordinate.x, rawPosition.y, gridCoordinate.y) * cellSize + halfCellOffset;
    }

    public Vector3 GetHalfCellOffset()
    {
        return new Vector3(cellSize, 0, cellSize) / 2.0f;
    }
}
