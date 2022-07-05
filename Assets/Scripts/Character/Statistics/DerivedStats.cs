using System.Collections.Generic;
using UnityEngine;

public class DerivedStats : MonoBehaviour
{
    private List<AbilityScore> derivedAbilityScoreList;
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
        InitialiseDerivedAbilityScoreList();

        CalculateAbilityScores();
        CalculateAbilityModifiers();

        DebugScores();
    }

    private void InitialiseDerivedAbilityScoreList()
    {
        derivedAbilityScoreList = new List<AbilityScore>();

        foreach (AbilityScore score in unitAbilities.GetAbilityScoreList())
        {
            AbilityScore baseScore = new AbilityScore();
            baseScore.SetAbility(score.GetAbility());
            baseScore.SetValue(score.GetValue());
            derivedAbilityScoreList.Add(baseScore);
        }
    }

    private void OnEnable()
    {
        unitAbilities.OnAbilityChanged += UnitAbilities_OnAbilityChanged;
    }

    private void OnDisable()
    {
        unitAbilities.OnAbilityChanged -= UnitAbilities_OnAbilityChanged;
    }

    private void CalculateAbilityScores()
    {
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
    }

    private void DebugScores()
    {
        for (int i = 0; i < derivedAbilityScoreList.Count; i++)
        {
            Debug.Log($"{derivedAbilityScoreList[i].GetAbility()} = {derivedAbilityScoreList[i].GetValue()}, with a modifier of {abilityScoreModifiers[i]}.");
        }
    }

    private void UnitAbilities_OnAbilityChanged()
    {
        CalculateAbilityScores();
        CalculateAbilityModifiers();
        DebugScores();
    }
}