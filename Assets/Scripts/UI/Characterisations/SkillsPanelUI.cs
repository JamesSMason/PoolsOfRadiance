using PoR.Character;
using PoR.Character.Customisation.Skills;
using PoR.Character.Settings;
using System;
using UnityEngine;

namespace PoR.UI.Characterisations
{
    public class SkillsPanelUI : MonoBehaviour
    {
        [SerializeField] private CharacterSheetUI characterSheetUI = null;
        [SerializeField] private SkillsUI skillUIPrefab = null;
        [SerializeField] private Transform skillUIParent = null;

        private Unit currentUnit;

        private void OnEnable()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += SelectedUnit_OnSelectedUnitChanged;
            currentUnit = UnitActionSystem.Instance.GetCurrentUnit();
            UpdateFields();
        }

        private void OnDisable()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged -= SelectedUnit_OnSelectedUnitChanged;
        }

        private void UpdateFields()
        {
            CharSkills charSkills = currentUnit.GetComponent<CharSkills>();

            foreach (Transform child in skillUIParent)
            {
                Destroy(child.gameObject);
            }

            string[] skillStrings = SkillTypes.GetSkills();
            for (int i = 0; i < skillStrings.Length; i++)
            {
                SkillsUI newSkill = Instantiate(skillUIPrefab, skillUIParent);
                newSkill.SetSkillName(skillStrings[i]);
                newSkill.SetSkillScore(charSkills.GetSkillModifierValue(i));
                newSkill.SetProficiencyImage(characterSheetUI.GetProficiencyImage(charSkills.GetSkillProficiencies(i)));
            }
        }

        private void SelectedUnit_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            currentUnit = sender as Unit;
            if (currentUnit != null)
            {
                UpdateFields();
            }
        }
    }
}