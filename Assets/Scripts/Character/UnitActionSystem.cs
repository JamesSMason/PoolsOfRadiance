using Por.Core;
using System;
using UnityEngine;

namespace PoR.Character
{
    public class UnitActionSystem : MonoBehaviour
    {
        public static UnitActionSystem Instance { get; private set; }

        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitLayerMask;

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
            if (TryHandleUnitSelection()) { return; }

            if (Input.GetMouseButtonDown(0))
            {
                selectedUnit.Move(MouseWorld.GetPosition());
            }
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

        public void SetSelectedUnit(Unit selectedUnit)
        {
            this.selectedUnit = selectedUnit;
            OnSelectedUnitChanged?.Invoke(selectedUnit, EventArgs.Empty);
        }

        public Unit GetCurrentUnit()
        {
            return selectedUnit;
        }
    }
}