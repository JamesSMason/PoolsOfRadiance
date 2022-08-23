using Por.Core;
using PoR.Actions;
using PoR.Controls;
using PoR.Grid;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PoR.Character
{
    public class UnitActionSystem : MonoBehaviour
    {
        public static UnitActionSystem Instance { get; private set; }

        [SerializeField] private Unit selectedUnit;
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
            SetSelectedUnit(selectedUnit);
        }

        private void Update()
        {
            if (isBusy)
            {
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject())
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
                        if (unit == selectedUnit)
                        {
                            return false;
                        }
                        SetSelectedUnit(unit);
                        return true;
                    }
                }
            }
            return false;
        }

        public void SetSelectedUnit(Unit unit)
        {
            selectedUnit = unit;
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

                if (selectedAction.IsValidActionGridPosition(mouseGridPosition))
                {
                    SetBusy();
                    selectedAction.TakeAction(mouseGridPosition, ClearBusy);
                }
            }
        }

        public Unit GetSelectedUnit()
        {
            return selectedUnit;
        }

        public BaseAction GetSelectedAction()
        {
            return selectedAction;
        }
    }
}