using System;
using System.Collections.Generic;
using UnityEngine;

public class DerivedStats : MonoBehaviour
{
    private Unit unit;
    private List<AbilityScore> derivedAbilityScoreList;
    private List<int> abilityScoreModifiers;
    private int speed;

    private UnitAbilities unitAbilities;
    private UnitRace unitRace;

    public event EventHandler OnAbilitiesChanged;

    public void UpdateUnit(Unit unit)
    {
        this.unit = unit;
        unitAbilities = unit.GetComponent<UnitAbilities>();
        unitRace = unit.GetComponent<UnitRace>();
        CalculateAbilityScores();
        CalculateAbilityModifiers();
    }


    private void CalculateAbilityScores()
    {
        derivedAbilityScoreList = new List<AbilityScore>();

        foreach (AbilityScore score in unitAbilities.GetAbilityScoreList())
        {
            AbilityScore baseScore = new AbilityScore();
            baseScore.SetAbility(score.GetAbility());
            baseScore.SetValue(score.GetValue());
            derivedAbilityScoreList.Add(baseScore);
        }

        foreach (AbilityScore bonus in unitRace.GetAbilityScoreList())
        {
            foreach (AbilityScore score in derivedAbilityScoreList)
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

        foreach (AbilityScore score in derivedAbilityScoreList)
        {
            abilityScoreModifiers.Add(AbilityModifiers.GetAbilityModifier(score.GetValue()));
        }
        OnAbilitiesChanged?.Invoke(this, EventArgs.Empty);
    }

    private void DebugScores()
    {
        for (int i = 0; i < derivedAbilityScoreList.Count; i++)
        {
            Debug.Log($"{derivedAbilityScoreList[i].GetAbility()} = {derivedAbilityScoreList[i].GetValue()}, with a modifier of {abilityScoreModifiers[i]}.");
        }
    }

    public List<AbilityScore> GetAbilityScoresList()
    {
        DebugScores();
        return derivedAbilityScoreList;
    }

    public List<int> GetAbilityScoreModifiersList()
    {
        return abilityScoreModifiers;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public string GetCharacterName()
    {
        return unit.gameObject.name;
    }
}