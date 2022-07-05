using System;
using UnityEngine;

[Serializable]
public class AbilityScore
{
    [SerializeField] private Abilities ability;
    [SerializeField] private int value;

    public Abilities GetAbility()
    {
        return ability;
    }

    public int GetValue()
    {
        return value;
    }

    public void SetValue(int value)
    {
        this.value = value;
    }
}