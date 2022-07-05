using System.Collections.Generic;
using UnityEngine;

public class UnitRace : MonoBehaviour
{
    [SerializeField] private RaceSO raceSO = null;

    private List<AbilityScore> abilityScoreList;
    private int speed;
    private Size size;

    private void Awake()
    {
        if (raceSO == null)
        {
            Debug.Log($"{gameObject.name} does not have a race Scriptable Object assigned).");
            return;
        }

        abilityScoreList = raceSO.GetAbilityScoreList();
        speed = raceSO.GetSpeed();
        size = raceSO.GetSize();
    }

    public List<AbilityScore> GetAbilityScoreList()
    {
        return abilityScoreList;
    }

    public Size GetSize()
    {
        return size;
    }

    public int GetSpeed()
    {
        return speed;
    }
}