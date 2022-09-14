using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public event EventHandler OnSelectedUnitChange;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;
    public bool isBusy;


    private void Update()
    {
        if (isBusy) return;

        if (LeftMouseClick())
        {
            if (HandleUnitSelection()) return;
            if (HandleUnitMovement()) return;
        }

        if (RightMouseClick())
        {
            SetBusy();
            selectedUnit.GetSpinAction().Spin(ClearBusy);
        }
    }

    public void SetBusy()
    {
        isBusy = true;
    }

    public void ClearBusy()
    {
        isBusy = false;
    }

    private bool LeftMouseClick()
    {
        return Input.GetMouseButtonDown(0);
    }

    private bool RightMouseClick()
    {
        return Input.GetMouseButtonDown(1);
    }

    private void SetSelectedUnit(Unit _unit)
    {
        selectedUnit = _unit;

        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    private bool HandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHitInfo, float.MaxValue, unitLayerMask))
        {
            if (raycastHitInfo.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);

                return true;
            }
        }

        return false;
    }

    private bool HandleUnitMovement()
    {
        SetBusy();

        if (selectedUnit != null)
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                selectedUnit.GetMoveAction().Move(mouseGridPosition);
            }

            return true;
        }

        return false;
    }
}
