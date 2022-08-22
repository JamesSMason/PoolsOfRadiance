using Por.Core;
using PoR.Actions;
using PoR.Controls;
using PoR.Grid;
using System;
using UnityEngine;

namespace PoR.Character
{
    public class UnitActionSystem : MonoBehaviour
    {
        public static UnitActionSystem Instance { get; private set; }

        [SerializeField] private Unit unit;
        [SerializeField] private LayerMask unitLayerMask;

        private bool isBusy;
        private BaseAction selectedAction;

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

        private void Start()
        {
            SetSelectedUnit(unit);
        }

        private void Update()
        {
            if (isBusy)
            {
                return;
            }

            if (TryHandleUnitSelection())
            {
                return;
            }

            HandleSelectedAction();
        }

        public void SetBusy()
        {
            isBusy = true;
        }

        public void ClearBusy()
        {
            isBusy = false;
        }

        private bool TryHandleUnitSelection()
        {
            if (Input.GetMouseButtonDown(0))
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
            }
            return false;
        }

        public void SetSelectedUnit(Unit unit)
        {
            this.unit = unit;
            SetSelectedAction(unit.GetMoveAction());
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetSelectedAction(BaseAction baseAction)
        {
            selectedAction = baseAction;
        }

        private void HandleSelectedAction()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

                switch (selectedAction)
                {
                    case MoveAction moveAction:
                        if (moveAction.IsValidActionGridPosition(mouseGridPosition))
                        {
                            SetBusy();
                            moveAction.Move(mouseGridPosition, ClearBusy);
                        }

                        break;

                    case SpinAction spinAction:
                        SetBusy();
                        spinAction.Spin(ClearBusy);

                        break;
                }
            }
        }

        public Unit GetCurrentUnit()
        {
            return unit;
        }
    }
}