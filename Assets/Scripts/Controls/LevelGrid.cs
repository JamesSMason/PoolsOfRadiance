using PoR.Character;
using PoR.Grid;
using System.Collections.Generic;
using UnityEngine;

namespace PoR.Controls
{
    public class LevelGrid : MonoBehaviour
    {
        public static LevelGrid Instance { get; private set; }

        [SerializeField] private int gridWidth = 10;
        [SerializeField] private int gridHeight = 10;
        [SerializeField] private int gridCellSize = 2;
        [SerializeField] private Transform gridObjectDebug = null;

        private GridSystem gridSystem;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log($"There is another LevelGrid already. {transform} - {Instance}.");
                Destroy(gameObject);
                return;
            }
            Instance = this;

            gridSystem = new GridSystem(gridWidth, gridHeight, gridCellSize);
            gridSystem.CreateDebugObjects(gridObjectDebug);
        }

        public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.AddUnit(unit);
        }

        public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.GetUnitList();
        }

        public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.RemoveUnit(unit);
        }

        public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
        {
            RemoveUnitAtGridPosition(fromGridPosition, unit);

            AddUnitAtGridPosition(toGridPosition, unit);
        }

        public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.HasAnyUnit();
        }

        public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

        public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

        public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

        public int GetWidth() => gridSystem.GetWidth();

        public int GetHeight() => gridSystem.GetHeight();
    }
}