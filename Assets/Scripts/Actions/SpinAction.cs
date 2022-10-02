using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    public override string GetActionName()
    {
        return "Spin";
    }

    private float totalSpinAmount;

    private void Update()
    {
        if (!isActive) return;

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        totalSpinAmount += spinAddAmount;

        if (totalSpinAmount >= 360f)
        {
            isActive = false;
            onActionComplete?.Invoke();
        }
    }

    public void Spin(Action _onActionCompleteCallback)
    {
        this.onActionComplete = _onActionCompleteCallback;
        isActive = true;
        totalSpinAmount = 0f;
    }
}
