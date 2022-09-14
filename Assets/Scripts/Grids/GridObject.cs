using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitList;

    public GridObject(GridSystem _gridSystem, GridPosition _gridPosition)
    {
        this.gridSystem = _gridSystem;
        this.gridPosition = _gridPosition;

        unitList = new List<Unit>();
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (Unit _unit in unitList)
        {
            unitString += _unit + "\n";
        }
        return gridPosition.ToString() + "\n" + unitString;
    }

    public void AddUnit(Unit _unit)
    {
        this.unitList.Add(_unit);
    }

    public void RemoveUnit(Unit _unit)
    {
        unitList.Remove(_unit);
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public bool HasAnyUnit()
    {
        return unitList.Count > 0;
    }
}
