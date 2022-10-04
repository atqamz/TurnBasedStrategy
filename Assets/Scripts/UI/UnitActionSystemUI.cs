using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] Transform unitActionButtonContainerTransform;
    [SerializeField] Transform unitActionButtonPrefab;
    [SerializeField] TextMeshProUGUI unitActionPointText;

    private List<UnitActionButtonUI> unitActionButtonUIList;

    private void Awake()
    {
        unitActionButtonUIList = new List<UnitActionButtonUI>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;

        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPointText();
    }
    // ==========================================================================================

    // subscribe
    // ==========================================================================================
    private void UnitActionSystem_OnSelectedUnitChanged(object _sender, EventArgs _e)
    {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPointText();
    }

    private void UnitActionSystem_OnSelectedActionChanged(object _sender, EventArgs _e)
    {
        UpdateSelectedVisual();
    }

    private void UnitActionSystem_OnActionStarted(object _sender, EventArgs _e)
    {
        UpdateActionPointText();
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

    private void UpdateActionPointText()
    {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        unitActionPointText.text = "Action Point: " + selectedUnit.GetActionPoint();
    }
}
