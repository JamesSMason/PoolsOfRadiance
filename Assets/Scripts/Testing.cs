using Por.Controls;
using PoR.Character;
using PoR.Character.Settings.Base;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit[] units;

    private int unitIndex = 0;

    private void Start()
    {
        units = FindObjectsOfType<Unit>();
        SelectedUnit.Instance.SetSelectedUnit(units[0]);
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
            SelectedUnit.Instance.SetSelectedUnit(units[unitIndex]);
            Debug.Log($"Selected unit is now {units[unitIndex].name}.");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SelectedUnit.Instance.GetCurrentUnit().GetComponent<BaseClass>().IncrementLevel();
        }
    }
}