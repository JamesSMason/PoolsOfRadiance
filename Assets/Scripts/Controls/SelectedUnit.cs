using PoR.Character;
using System;
using UnityEngine;

namespace Por.Controls
{
    public class SelectedUnit : MonoBehaviour
    {
        public static SelectedUnit Instance { get; private set; }

        public event EventHandler OnSelectedUnitChanged;

        [SerializeField] Unit currentUnit;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log($"There is another Selected Unit already. {transform}.");
                Destroy(gameObject);
            }
            Instance = this;
        }

        public void SetSelectedUnit(Unit selectedUnit)
        {
            this.currentUnit = selectedUnit;
            OnSelectedUnitChanged?.Invoke(selectedUnit, EventArgs.Empty);
        }

        public Unit GetCurrentUnit()
        {
            return currentUnit;
        }
    }
}