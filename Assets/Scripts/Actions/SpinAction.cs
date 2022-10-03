using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    // parent override
    // ==========================================================================================
    public override string GetActionName()
    {
        return "Spin";
    }

    public override void TakeAction(GridPosition _gridPosition, Action _onActionCompleteCallback)
    {
        this.onActionComplete = _onActionCompleteCallback;
        isActive = true;
        totalSpinAmount = 0f;
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition> { unitGridPosition };
    }
    // ==========================================================================================

    // child
    // ==========================================================================================
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
}
