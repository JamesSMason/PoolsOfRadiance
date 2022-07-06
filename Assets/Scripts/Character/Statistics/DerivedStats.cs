using System;
using System.Collections.Generic;
using UnityEngine;

public class DerivedStats : MonoBehaviour
{
    private List<AbilityScore> derivedAbilityScoreList;
    private List<int> abilityScoreModifiersList;
    private List<int> abilityScoreSavingThrowBonusList;
    private int speed;
    private int level;

    private UnitAbilities unitAbilities;
    private UnitRace unitRace;
    private UnitClass[] unitClasses;

    public event EventHandler OnAbilitiesChanged;

    public void Start()
    {
        unitAbilities = GetComponent<UnitAbilities>();
        unitRace = GetComponent<UnitRace>();
        unitClasses = GetComponents<UnitClass>();
        CalculateLevel();
        CalculateAbilityScores();
        CalculateAbilityModifiers();
        CalculateAbilitySaves();
        OnAbilitiesChanged?.Invoke(this, EventArgs.Empty);
    }

    private void CalculateLevel()
    {
        level = 0;
        foreach (UnitClass unitClass in unitClasses)
        {
            level += unitClass.GetClassLevel();
        }
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
        abilityScoreModifiersList = new List<int>(6);

        foreach (AbilityScore score in derivedAbilityScoreList)
        {
            abilityScoreModifiersList.Add(AbilityModifiers.GetAbilityModifier(score.GetValue()));
        }
    }

    private void CalculateAbilitySaves()
    {
        abilityScoreSavingThrowBonusList = new List<int>(6);

        for (int i = 0; i < derivedAbilityScoreList.Count; i++)
        {
            int save = abilityScoreModifiersList[i];
            for (int j = 0; j < unitClasses.Length; j++)
            {
                if (!unitClasses[j].GetIsSaveProficient(derivedAbilityScoreList[i].GetAbility()))
                {
                    continue;
                }
                save += ProficiencyBonuses.GetProficiencyBonus(level);
            }
            abilityScoreSavingThrowBonusList.Add(save);
        }
    }

    private void DebugScores()
    {
        Debug.Log($"{gameObject.name} is a level {level} {unitRace.GetPlayerRace()} {unitClasses[0].GetPlayerClass()}");
        for (int i = 0; i < derivedAbilityScoreList.Count; i++)
        {
            Debug.Log($"{derivedAbilityScoreList[i].GetAbility()} = {derivedAbilityScoreList[i].GetValue()}, ");
            Debug.Log($"with a modifier of {abilityScoreModifiersList[i]}.");
            Debug.Log($"and a save of  {abilityScoreSavingThrowBonusList[i]}.");
        }
    }

    public List<AbilityScore> GetAbilityScoresList()
    {
        DebugScores();
        return derivedAbilityScoreList;
    }

    public List<int> GetAbilityScoreModifiersList()
    {
        return abilityScoreModifiersList;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public string GetCharacterName()
    {
        return gameObject.name;
    }
}