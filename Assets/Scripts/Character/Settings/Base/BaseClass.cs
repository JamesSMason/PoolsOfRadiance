using PoR.Character.Customisation.Classes;
using PoR.Character.Customisation.Statistics;
using System;
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
        private int classLevel;

        public event Action OnLevelUp;

        private void Awake()
        {
            characterClass = classSO.GetClass();
            hitDie = classSO.GetHitDie();
            abilitySaveProficiencyList = classSO.GetAbilitySaveProficiencyList();
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

        public bool GetIsSaveProficient(int index)
        {
            return abilitySaveProficiencyList.Contains(GetComponent<CharAbilityScores>().GetAbility(index));
        }

        public int GetClassLevel()
        {
            return classLevel;
        }

        public void IncrementLevel()
        {
            classLevel++;
            OnLevelUp?.Invoke();
        }
    }
}