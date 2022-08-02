using Por.Controls;
using PoR.Character;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit[] units;

    private int unitIndex = 0;

    private void Start()
    {
        units = FindObjectsOfType<Unit>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            unitIndex++;
            if (unitIndex >= units.Length)
            {
                unitIndex = 0;
            }
        }

        SelectedUnit.Instance.SetSelectedUnit(units[unitIndex]);
    }
}