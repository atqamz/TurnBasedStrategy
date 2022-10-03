using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitActionButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private GameObject selectedGameObject;

    private BaseAction baseAction;

    public void SetBaseAction(BaseAction _baseAction)
    {
        this.baseAction = _baseAction;

        textMeshPro.text = _baseAction.GetActionName().ToUpper();

        button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(_baseAction);
        });
    }

    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();

        selectedGameObject.SetActive(selectedBaseAction == baseAction);
    }
}
