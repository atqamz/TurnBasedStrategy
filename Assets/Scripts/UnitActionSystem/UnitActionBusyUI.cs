using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionBusyUI : MonoBehaviour
{
    private void Start()
    {
        UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;

        Hide();
    }
    /// ==========================================================================================

    // subscribe
    // ==========================================================================================
    private void UnitActionSystem_OnBusyChanged(object _sender, bool _isBusy)
    {
        if (_isBusy)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    // ==========================================================================================

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
