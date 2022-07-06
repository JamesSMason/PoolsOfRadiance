using System.Collections.Generic;
using UnityEngine;

public class UnitClass : MonoBehaviour
{
    [SerializeField] private ClassSO classSO;

    private string playerClass;
    private int hitDie;
    private List<Abilities> abilitySaveProficiencyList;
    private List<int> hitPointsPerLevel = new List<int>();
    private int classLevel;

    private void Awake()
    {
        playerClass = classSO.GetClass();
        hitDie = classSO.GetHitDie();
        abilitySaveProficiencyList = classSO.GetAbilitySaveProficiencyList();
        hitPointsPerLevel.Add(hitDie);
        classLevel = 1;
    }

    public string GetPlayerClass()
    {
        return playerClass;
    }

    public int GetHitDie()
    {
        return hitDie;
    }

    public bool GetIsSaveProficient(Abilities ability)
    {
        return abilitySaveProficiencyList.Contains(ability);
    }

    public int GetMaxHitPoints()
    {
        int maxHP = 0;
        for (int i = 1; i < hitPointsPerLevel.Count; i++)
        {
            maxHP += hitPointsPerLevel[i];
        }
        return maxHP;
    }

    public int GetClassLevel()
    {
        return classLevel;
    }
}
