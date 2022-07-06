using System;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit[] unit;
    [SerializeField] private DerivedStats stats;

    private int unitIndex = 0;

    private void Start()
    {
        unit = FindObjectsOfType<Unit>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            FindObjectOfType<DerivedStats>().UpdateUnit(unit[unitIndex]);
            unitIndex++;
            if (unitIndex >= unit.Length)
            {
                unitIndex = 0;
            }
        }
    }
}