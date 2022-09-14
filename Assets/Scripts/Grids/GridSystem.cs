using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] gridObjectArray;

    public GridSystem(int _width, int _height, float _cellSize)
    {
        this.width = _width;
        this.height = _height;
        this.cellSize = _cellSize;
        gridObjectArray = new GridObject[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public Vector3 GetWorldPosition(GridPosition _gridPosition)
    {
        return new Vector3(_gridPosition.x, 0, _gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 _worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(_worldPosition.x / cellSize), Mathf.RoundToInt(_worldPosition.z / cellSize));
    }

    public void CreateDebugObjects(Transform _debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform debugTransform = GameObject.Instantiate(_debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);

                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition _gridPosition)
    {
        return gridObjectArray[_gridPosition.x, _gridPosition.z];
    }

    public bool IsValidGridPosition(GridPosition _gridPosition)
    {
        return _gridPosition.x >= 0 &&
               _gridPosition.z >= 0 &&
               _gridPosition.x < width &&
               _gridPosition.z < height;
    }
}
