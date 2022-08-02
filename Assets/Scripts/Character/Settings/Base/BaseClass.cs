using PoR.Character.Customisation.Classes;
using PoR.Character.Customisation.Statistics;
using System.Collections.Generic;
using UnityEngine;

namespace PoR.Character.Settings.Base
{
    public class BaseClass : MonoBehaviour
    {
        [SerializeField] private ClassSO classSO;

        private string characterClass;
        private int hitDie;
        private List<Abilities> abilitySaveProficiencyList;
        private List<int> hitPointsPerLevel = new List<int>();
        private int classLevel;

        private void Awake()
        {
            characterClass = classSO.GetClass();
            hitDie = classSO.GetHitDie();
            abilitySaveProficiencyList = classSO.GetAbilitySaveProficiencyList();
            hitPointsPerLevel.Add(GetHitDie());
            classLevel = 1;
        }

        public string GetCharacterClass()
        {
            return characterClass;
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
            for (int i = 0; i < hitPointsPerLevel.Count; i++)
            {
                maxHP += hitPointsPerLevel[i];
            }
            return maxHP;
        }

        public int GetClassLevel()
        {
            return classLevel;
        }

        public void IncrementLevel()
        {
            classLevel++;
            hitPointsPerLevel.Add(Random.Range(1, GetHitDie() + 1));
        }
    }
}