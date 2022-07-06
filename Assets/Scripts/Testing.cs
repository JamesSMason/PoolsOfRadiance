using System;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit[] unit;

    private int unitIndex = 0;

    private void Start()
    {
        unit = FindObjectsOfType<Unit>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            FindObjectOfType<SelectedUnit>().SetSelectedUnit(unit[unitIndex]);
            unitIndex++;
            if (unitIndex >= unit.Length)
            {
                unitIndex = 0;
            }
        }
    }
}