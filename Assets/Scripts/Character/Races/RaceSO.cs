using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Race", menuName = "PoR/New Race", order = 0)]
public class RaceSO : ScriptableObject
{
    [SerializeField] private Races race;
    [SerializeField] private List<AbilityScore> abilityScoreList;
    [SerializeField] private Size size;
    [SerializeField] private int speed;

    public string GetRace()
    {
        return race.ToString();
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