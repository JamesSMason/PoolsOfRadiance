using System;
using UnityEngine;

public class SelectedUnit : MonoBehaviour
{
    public event EventHandler OnSelectedUnitChanged;

    Unit selectedUnit;

    public void SetSelectedUnit(Unit selectedUnit)
    {
        this.selectedUnit = selectedUnit;
        OnSelectedUnitChanged?.Invoke(selectedUnit, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
