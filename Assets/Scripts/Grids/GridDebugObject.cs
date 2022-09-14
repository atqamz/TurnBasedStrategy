using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro gridText;
    private GridObject gridObject;

    private void Update()
    {
        gridText.text = gridObject.ToString();
    }

    public void SetGridObject(GridObject _gridObject)
    {
        this.gridObject = _gridObject;
    }
}
