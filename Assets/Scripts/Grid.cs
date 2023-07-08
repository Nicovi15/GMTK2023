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

    // private void Start()
    // {
    //     Debug.Log(GetSnapPosition(new Vector3(6, 0, 4)));
    // }

    public Vector3 GetSnapPosition(in Vector3 rawPosition)
    {
        Vector3 gridPosition = new Vector3((int)(rawPosition.x / cellSize),
                                                rawPosition.y, 
                                            (int)(rawPosition.z / cellSize));

        // Snap position to the center of grid
        Vector3 halfCellOffset = new Vector3(cellSize, 0, cellSize) / 2.0f;
        return gridPosition * cellSize + halfCellOffset;
    }

    public Vector3 GetHalfCellOffset()
    {
        return new Vector3(cellSize, 0, cellSize) / 2.0f;
    }
}
