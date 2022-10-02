using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    public override string GetActionName()
    {
        return "Move";
    }

    [SerializeField] private Animator unitAnimator;
    private Vector3 targetPosition;
    [SerializeField] private int maxMoveDistance = 4;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (!isActive) return;


        if (!ReachDestination(targetPosition))
        {
            float moveSpeed = 4f;
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            unitAnimator.SetBool("IsMoving", true);
        }
        else
        {
            unitAnimator.SetBool("IsMoving", false);
            isActive = false;

            onActionComplete();
        }

    }

    private bool ReachDestination(Vector3 _targetPosition)
    {
        float stoppingDistance = 0.01f;
        float distance = Vector3.Distance(transform.position, _targetPosition);

        if (distance <= stoppingDistance)
            return true;
        else
            return false;
    }

    public void Move(GridPosition _gridPosition, Action _onActionCompleteCallback)
    {
        this.onActionComplete = _onActionCompleteCallback;

        this.targetPosition = LevelGrid.Instance.GetWorldPosition(_gridPosition);
        isActive = true;
    }

    public bool IsValidActionGridPosition(GridPosition _gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(_gridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();


        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    continue;

                if (unitGridPosition == testGridPosition)
                    continue;

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    continue;

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
