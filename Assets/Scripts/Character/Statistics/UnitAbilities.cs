using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbilities : MonoBehaviour
{
    [SerializeField] private List<AbilityScore> abilityScoreList;

    public event Action OnAbilityChanged;

    public List<AbilityScore> GetAbilityScoreList()
    {
        return abilityScoreList;
    }

    public int GetAbility(Abilities ability)
    {
        foreach (AbilityScore abilityScore in abilityScoreList)
        {
            if (abilityScore.GetAbility() == ability)
            {
                return abilityScore.GetValue();
            }
        }
        return 0;
    }

    public void SetAbility(Abilities ability, int value)
    {
        int indexToChange = -1;
        for (int i = 0; i < abilityScoreList.Count; i++)
        {
            if (abilityScoreList[i].GetAbility() != ability)
            {
                continue;
            }
            indexToChange = i;
        }
        // TODO: Remove check when game finished
        if (indexToChange == -1)
        {
            Debug.Log("Cannot find ability!");
            return;
        }
        abilityScoreList[indexToChange].SetValue(value);

        OnAbilityChanged?.Invoke();
    }
}