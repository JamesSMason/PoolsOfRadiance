using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Class", menuName = "PoR/New Class", order = 0)]
public class ClassSO : ScriptableObject
{
    [SerializeField] Classes playerClass;
    [SerializeField] int hitDie;
    [SerializeField] List<Abilities> abilitySaveProficiencyList;

    public string GetClass()
    {
        return playerClass.ToString();
    }

    public int GetHitDie()
    {
        return hitDie;
    }

    public List<Abilities> GetAbilitySaveProficiencyList()
    {
        return abilitySaveProficiencyList;
    }
}