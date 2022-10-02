using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] Transform unitActionButtonContainerTransform;
    [SerializeField] Transform unitActionButtonPrefab;

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;

        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons()
    {
        foreach (Transform unitActionButton in unitActionButtonContainerTransform)
        {
            Destroy(unitActionButton.gameObject);
        }

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(unitActionButtonPrefab, unitActionButtonContainerTransform);
            UnitActionButtonUI unitActionButtonUI = actionButtonTransform.GetComponent<UnitActionButtonUI>();
            unitActionButtonUI.SetBaseAction(baseAction);
        }
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateUnitActionButtons();
    }
}
