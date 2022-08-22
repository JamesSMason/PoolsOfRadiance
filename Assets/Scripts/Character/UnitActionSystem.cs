using Por.Core;
using PoR.Controls;
using PoR.Grid;
using System;
using UnityEngine;

namespace PoR.Character
{
    public class UnitActionSystem : MonoBehaviour
    {
        public static UnitActionSystem Instance { get; private set; }

        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitLayerMask;

        private bool isBusy;

        public event EventHandler OnSelectedUnitChanged;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log($"There is another UnitActionSystem already. {transform} - {Instance}.");
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            if (isBusy)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (TryHandleUnitSelection()) { return; }
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
                if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
                {
                    SetBusy();
                    selectedUnit.GetMoveAction().Move(mouseGridPosition, ClearBusy);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                SetBusy();
                selectedUnit.GetSpinAction().Spin(ClearBusy);
            }
        }

        private bool TryHandleUnitSelection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }
            return false;
        }

        public void SetSelectedUnit(Unit selectedUnit)
        {
            this.selectedUnit = selectedUnit;
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }

        public Unit GetCurrentUnit()
        {
            return selectedUnit;
        }

        public void SetBusy()
        {
            isBusy = true;
        }

        public void ClearBusy()
        {
            isBusy = false;
        }
    }
}