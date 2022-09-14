using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    [SerializeField] Unit unit;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GridVisual.Instance.HideAllGridVisual();
            GridVisual.Instance.ShowGridVisualList(unit.GetMoveAction().GetValidActionGridPositionList());
        }
    }
}
