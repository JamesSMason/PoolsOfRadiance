using System.Collections.Generic;
using UnityEngine;

public class DerivedStats : MonoBehaviour
{
    private List<AbilityScore> adjustedAbilityScoreList;
    private List<AbilityScore> raceAbilityScoreList;
    private List<int> abilityScoreModifiers;
    private int speed;

    private UnitAbilities unitAbilities;
    private UnitRace unitRace;

    private void Awake()
    {
        unitAbilities = GetComponent<UnitAbilities>();
        unitRace = GetComponent<UnitRace>();
    }

    private void Start()
    {
        CalculateAbilityScores();
        CalculateAbilityModifiers();

        DebugScores();
    }

    private void OnEnable()
    {
        unitAbilities.OnAbilityChanged += UnitAbilities_OnAbilityChanged;
    }

    private void CalculateAbilityScores()
    {
        adjustedAbilityScoreList = unitAbilities.GetAbilityScoreList();
        raceAbilityScoreList = unitRace.GetAbilityScoreList();

        foreach (AbilityScore bonus in raceAbilityScoreList)
        {
            foreach (AbilityScore score in adjustedAbilityScoreList)
            {
                if (bonus.GetAbility() == score.GetAbility())
                {
                    score.SetValue(score.GetValue() + bonus.GetValue());
                    break;
                }
            }
        }
    }

    private void CalculateAbilityModifiers()
    {
        abilityScoreModifiers = new List<int>(6);

        foreach (AbilityScore score in adjustedAbilityScoreList)
        {
            abilityScoreModifiers.Add(AbilityModifiers.GetAbilityModifier(score.GetValue()));
        }
    }

    private void DebugScores()
    {
        for (int i = 0; i < adjustedAbilityScoreList.Count; i++)
        {
            Debug.Log($"{adjustedAbilityScoreList[i].GetAbility()} = {adjustedAbilityScoreList[i].GetValue()}, with a modifier of {abilityScoreModifiers[i]}.");
        }
    }

    private void UnitAbilities_OnAbilityChanged()
    {
        CalculateAbilityScores();
        CalculateAbilityModifiers();
        DebugScores();
    }
}