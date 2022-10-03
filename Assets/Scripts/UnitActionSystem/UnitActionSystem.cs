using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<bool> OnBusyChanged;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private BaseAction selectedAction;

    public bool isBusy;

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }

    private void Update()
    {
        if (isBusy) return;

        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (HandleSelectedUnit()) return;

        HandleSelectedAction();
    }

    public void SetBusy()
    {
        isBusy = true;

        OnBusyChanged?.Invoke(this, isBusy);
    }

    public void ClearBusy()
    {
        isBusy = false;

        OnBusyChanged?.Invoke(this, isBusy);
    }

    private void SetSelectedUnit(Unit _unit)
    {
        selectedUnit = _unit;
        SetSelectedAction(_unit.GetMoveAction());

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public void SetSelectedAction(BaseAction _action)
    {
        selectedAction = _action;

        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }

    private bool HandleSelectedUnit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHitInfo, float.MaxValue, unitLayerMask))
            {
                if (raycastHitInfo.transform.TryGetComponent<Unit>(out Unit _unit))
                {
                    // if unit already selected
                    if (_unit == selectedUnit) return false;

                    SetSelectedUnit(_unit);

                    return true;
                }
            }
        }

        return false;
    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            }
        }
    }
}
