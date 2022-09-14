using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    [SerializeField] private Transform gridDebugObjectPrefab;
    private GridSystem gridSystem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public void AddUnitAtGridPosition(GridPosition _gridPosition, Unit _unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(_gridPosition);
        gridObject.AddUnit(_unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition _gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(_gridPosition);
        return gridObject.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition _gridPosition, Unit _unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(_gridPosition);
        gridObject.RemoveUnit(_unit);
    }

    public void UnitMovedGridPosition(Unit _unit, GridPosition _fromGridPosition, GridPosition _toGridPosition)
    {
        RemoveUnitAtGridPosition(_fromGridPosition, _unit);
        AddUnitAtGridPosition(_toGridPosition, _unit);
    }

    public GridPosition GetGridPosition(Vector3 _worldPosition)
    {
        return gridSystem.GetGridPosition(_worldPosition);
    }

    public Vector3 GetWorldPosition(GridPosition _gridPosition)
    {
        return gridSystem.GetWorldPosition(_gridPosition);
    }

    public int GetWidth()
    {
        return gridSystem.GetWidth();
    }

    public int GetHeight()
    {
        return gridSystem.GetHeight();
    }

    public bool IsValidGridPosition(GridPosition _gridPosition)
    {
        return gridSystem.IsValidGridPosition(_gridPosition);
    }

    public bool HasAnyUnitOnGridPosition(GridPosition _gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(_gridPosition);
        return gridObject.HasAnyUnit();
    }
}
