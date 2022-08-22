using PoR.Character;
using PoR.Controls;
using System.Collections.Generic;
using UnityEngine;

namespace PoR.Grid
{
    public class GridSystemVisual : MonoBehaviour
    {
        public static GridSystemVisual Instance { get; private set; }

        [SerializeField] private Transform gridSystemVisualSinglePrefab = null;

        private GridSystemVisualSingle[,] gridSystemVisualSingleArray;
        private int gridWidth;
        private int gridHeight;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log($"There is another GridSystemVisual already. {transform} - {Instance}.");
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            gridWidth = LevelGrid.Instance.GetWidth();
            gridHeight = LevelGrid.Instance.GetHeight();
            gridSystemVisualSingleArray = new GridSystemVisualSingle[gridWidth, gridHeight];

            for (int x = 0; x < gridWidth; x++)
            {
                for (int z = 0; z < gridHeight; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    Transform gridSystemVisualSingleInstance = Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                    gridSystemVisualSingleArray[x,z] = gridSystemVisualSingleInstance.GetComponent<GridSystemVisualSingle>();
                }
            }
            HideAllGridPositions();
        }

        private void Update()
        {
            UpdateGridVisual();
        }

        public void HideAllGridPositions()
        {
            foreach (GridSystemVisualSingle gridSystemVisualSingle in gridSystemVisualSingleArray)
            {
                gridSystemVisualSingle.Hide();
            }
        }

        public void ShowGridPositionList(List<GridPosition> gridPositionList)
        {
            foreach (GridPosition gridPosition in gridPositionList)
            {
                gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
            }
        }

        private void UpdateGridVisual()
        {
            HideAllGridPositions();
            Unit selectedUnit = UnitActionSystem.Instance.GetCurrentUnit();
            ShowGridPositionList(selectedUnit.GetMoveAction().GetValidActionGridPositionList());
        }
    }
}