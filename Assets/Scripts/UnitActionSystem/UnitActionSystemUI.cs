using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] Transform unitActionButtonContainerTransform;
    [SerializeField] Transform unitActionButtonPrefab;

    private List<UnitActionButtonUI> unitActionButtonUIList;

    private void Awake()
    {
        unitActionButtonUIList = new List<UnitActionButtonUI>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;

        CreateUnitActionButtons();
        UpdateSelectedVisual();
    }
    // ==========================================================================================

    // subscribe
    // ==========================================================================================
    private void UnitActionSystem_OnSelectedUnitChanged(object _sender, EventArgs _e)
    {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
    }

    private void UnitActionSystem_OnSelectedActionChanged(object _sender, EventArgs _e)
    {
        UpdateSelectedVisual();
    }
    // ==========================================================================================


    // ==========================================================================================
    private void CreateUnitActionButtons()
    {
        foreach (Transform _unitActionButton in unitActionButtonContainerTransform)
        {
            Destroy(_unitActionButton.gameObject);
        }

        unitActionButtonUIList.Clear();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction _baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(unitActionButtonPrefab, unitActionButtonContainerTransform);
            UnitActionButtonUI unitActionButtonUI = actionButtonTransform.GetComponent<UnitActionButtonUI>();
            unitActionButtonUI.SetBaseAction(_baseAction);

            unitActionButtonUIList.Add(unitActionButtonUI);
        }
    }

    private void UpdateSelectedVisual()
    {
        foreach (UnitActionButtonUI _unitActionButtonUI in unitActionButtonUIList)
        {
            _unitActionButtonUI.UpdateSelectedVisual();
        }
    }
}
