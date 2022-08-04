using PoR.Character.Customisation.Skills;
using PoR.Character.Customisation.Statistics;
using System;
using UnityEngine;

namespace PoR.Character.Settings
{
    public class CharSkills : MonoBehaviour
    {
        [SerializeField] CharClass charClass = null;
        [SerializeField] CharAbilityScores charAbilityScores = null;

        private string[] skills;
        private Abilities[] skillModifierStats;
        private int[] skillModifierValues;
        private bool[] skillProficiencies;

        int arrayLength;

        private void Start()
        {
            skills = SkillTypes.GetSkills();
            skillModifierStats = SkillTypes.GetSkillModifierStats();
            arrayLength = skills.Length;

            skillModifierValues = new int[arrayLength];
            skillProficiencies = new bool[arrayLength];
        }

        private void OnEnable()
        {
            if (charAbilityScores != null)
            {
                charAbilityScores.OnAbilitiesChanged += CharAbilityScores_OnAbilitiesChanged;
            }
        }

        private void OnDisable()
        {
            if (charAbilityScores != null)
            {
                charAbilityScores.OnAbilitiesChanged -= CharAbilityScores_OnAbilitiesChanged;
            }
        }

        public int GetSkillModifierValue(int index)
        {
            return skillModifierValues[index];
        }

        public bool GetSkillProficiencies(int index)
        {
            return skillProficiencies[index];
        }

        private void UpdateSkills()
        {
            CharLevel charLevel = GetComponent<CharLevel>();
            for (int i = 0; i < arrayLength; i++)
            {
                skillModifierValues[i] = charAbilityScores.GetAbilityScoreModifier(skillModifierStats[i]);
                skillProficiencies[i] = false;
                if (charClass.GetSkillProficiencies().Contains((Skill)i))
                {
                    skillProficiencies[i] = true;
                    skillModifierValues[i] += ProficiencyBonus.GetProficiencyBonus(charLevel.GetLevel());
                }
            }
        }

        private void CharAbilityScores_OnAbilitiesChanged(object sender, EventArgs e)
        {
            UpdateSkills();
        }
    }
}