using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitActionButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    public void SetBaseAction(BaseAction _baseAction)
    {
        textMeshPro.text = _baseAction.GetActionName().ToUpper();
    }
}
