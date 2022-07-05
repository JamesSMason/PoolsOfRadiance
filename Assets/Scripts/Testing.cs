using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            unit.GetComponent<UnitAbilities>().SetAbility(Abilities.Strength, 18);
        }
    }
}