using PoR.Character.Customisation.Statistics;
using PoR.Character.Settings.Base;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoR.Character.Settings
{
    public class CharAbilityScores : MonoBehaviour
    {
        private List<AbilityScore> derivedAbilityScoreList;
        private List<AbilityScore> abilityScoreModifiersList;
        private List<AbilityScore> abilityScoreSavingThrowBonusList;

        private BaseAbilities baseAbilities;
        private BaseRace baseRace;
        private BaseClass[] baseClasses;

        public event EventHandler OnAbilitiesChanged;

        private void Awake()
        {
            baseAbilities = GetComponent<BaseAbilities>();
            baseRace = GetComponent<BaseRace>();
            baseClasses = GetComponents<BaseClass>();
        }

        private void Start()
        {
            CalculateAbilityScores();
            CalculateAbilityModifiers();
            CalculateAbilitySaves();
            OnAbilitiesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void CalculateAbilityScores()
        {
            derivedAbilityScoreList = new List<AbilityScore>();

            foreach (AbilityScore score in baseAbilities.GetAbilityScoreList())
            {
                AbilityScore baseScore = new AbilityScore();
                baseScore.SetAbility(score.GetAbility());
                baseScore.SetValue(score.GetValue());
                derivedAbilityScoreList.Add(baseScore);
            }

            foreach (AbilityScore bonus in baseRace.GetAbilityScoreList())
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
            abilityScoreModifiersList = new List<AbilityScore>();

            foreach (AbilityScore score in derivedAbilityScoreList)
            {
                AbilityScore baseScore = new AbilityScore();
                baseScore.SetAbility(score.GetAbility());
                baseScore.SetValue(AbilityModifiers.GetAbilityModifier(score.GetValue()));
                abilityScoreModifiersList.Add(baseScore);
            }
        }

        private void CalculateAbilitySaves()
        {
            abilityScoreSavingThrowBonusList = new List<AbilityScore>();


            foreach (AbilityScore score in abilityScoreModifiersList)
            {
                AbilityScore baseScore = new AbilityScore();
                baseScore.SetAbility(score.GetAbility());
                baseScore.SetValue(0);
                abilityScoreSavingThrowBonusList.Add(baseScore);
            }

            for (int i = 0; i < derivedAbilityScoreList.Count; i++)
            {
                int save = abilityScoreModifiersList[i].GetValue();
                for (int j = 0; j < baseClasses.Length; j++)
                {
                    if (!baseClasses[j].GetIsSaveProficient(i))
                    {
                        continue;
                    }
                    save += ProficiencyBonus.GetProficiencyBonus(GetComponent<CharLevel>().GetLevel());
                }
                abilityScoreSavingThrowBonusList[i].SetValue(save);
            }
        }

        public int GetPassivePerception()
        {
            int passivePerception = 10;
            foreach (AbilityScore score in GetAbilityScoreSavingThrowBonusList())
            {
                if (score.GetAbility() != Abilities.Wisdom)
                {
                    continue;
                }
                passivePerception += score.GetValue();
                break;
            }
            return passivePerception;
        }

        public Abilities GetAbility(int index)
        {
            return derivedAbilityScoreList[index].GetAbility();
        }

        public List<AbilityScore> GetAbilityScoresList()
        {
            return derivedAbilityScoreList;
        }

        public List<AbilityScore> GetAbilityScoreModifiersList()
        {
            return abilityScoreModifiersList;
        }

        public int GetAbilityScoreModifier(Abilities ability)
        {
            foreach (AbilityScore modifier in abilityScoreModifiersList)
            {
                if (modifier.GetAbility() != ability)
                {
                    continue;
                }
                return modifier.GetValue();
            }
            return 0;
        }

        public List<AbilityScore> GetAbilityScoreSavingThrowBonusList()
        {
            return abilityScoreSavingThrowBonusList;
        }
    }
}