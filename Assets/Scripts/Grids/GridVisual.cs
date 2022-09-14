using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisual : MonoBehaviour
{
    public static GridVisual Instance { get; private set; }

    [SerializeField] Transform gridVisualObjectPrefab;

    private GridVisualObject[,] gridVisualObjectArray;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        gridVisualObjectArray = new GridVisualObject[
            LevelGrid.Instance.GetWidth(),
            LevelGrid.Instance.GetHeight()
        ];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridVisualObjectTransform = Instantiate(gridVisualObjectPrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);

                gridVisualObjectArray[x, z] = gridVisualObjectTransform.GetComponent<GridVisualObject>();
            }
        }
    }

    private void Update()
    {
        UpdateGridVisual();
    }

    public void HideAllGridVisual()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                gridVisualObjectArray[x, z].Hide();
            }
        }
    }

    public void ShowGridVisualList(List<GridPosition> _gridPositionList)
    {
        foreach (GridPosition gridPosition in _gridPositionList)
        {
            gridVisualObjectArray[gridPosition.x, gridPosition.z].Show();
        }
    }

    private void UpdateGridVisual()
    {
        HideAllGridVisual();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        if (selectedUnit != null)
            ShowGridVisualList(selectedUnit.GetMoveAction().GetValidActionGridPositionList());
    }
}
