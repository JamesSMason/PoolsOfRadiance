using Por.Controls;
using PoR.Character;
using PoR.Character.Customisation.Statistics;
using PoR.Character.Settings;
using PoR.Character.Settings.Base;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PoR.UI.Characterisations
{
    public class StatisticsPanelUI : MonoBehaviour
    {
        [SerializeField] private CharacterSheetUI characterSheetUI = null;
        [SerializeField] private TextMeshProUGUI[] abilityValuesText = new TextMeshProUGUI[6];
        [SerializeField] private TextMeshProUGUI[] abilityModifiersText = new TextMeshProUGUI[6];
        [SerializeField] private TextMeshProUGUI[] abilitySaveValuesText = new TextMeshProUGUI[6];
        [SerializeField] private Image[] saveProficiencyImage = new Image[6];
        [SerializeField] private TextMeshProUGUI passivePerceptionText = null;
        [SerializeField] private TextMeshProUGUI proficiencyBonusText = null;

        private Unit currentUnit;

        private void OnEnable()
        {
            SelectedUnit.Instance.OnSelectedUnitChanged += SelectedUnit_OnSelectedUnitChanged;
            currentUnit = SelectedUnit.Instance.GetCurrentUnit();
            UpdateFields();
        }

        private void OnDisable()
        {
            SelectedUnit.Instance.OnSelectedUnitChanged -= SelectedUnit_OnSelectedUnitChanged;
        }

        private void UpdateFields()
        {
            CharAbilityScores currentCharAbilityScores = currentUnit.GetComponent<CharAbilityScores>();
            List<AbilityScore> abilityScoreList = currentCharAbilityScores.GetAbilityScoresList();
            List<AbilityScore> abilityModifierList = currentCharAbilityScores.GetAbilityScoreModifiersList();
            List<AbilityScore> abilitySaveList = currentCharAbilityScores.GetAbilityScoreSavingThrowBonusList();

            for (int i = 0; i < currentCharAbilityScores.GetAbilityScoresList().Count; i++)
            {
                abilityValuesText[i].text = abilityScoreList[i].GetValue().ToString();

                abilityModifiersText[i].text = abilityModifierList[i].GetValue().ToString();

                abilitySaveValuesText[i].text = abilitySaveList[i].GetValue().ToString();

                saveProficiencyImage[i].sprite = characterSheetUI.GetProficiencyImage(currentUnit.GetComponent<BaseClass>().GetIsSaveProficient(i));
            }

            int passivePerception = 10;
            foreach (AbilityScore score in abilityScoreList)
            {
                if (score.GetAbility() == Abilities.Wisdom)
                {
                    passivePerception += score.GetValue();
                    break;
                }
            }

            passivePerceptionText.text = $"{passivePerception}";

            proficiencyBonusText.text = $"{ProficiencyBonus.GetProficiencyBonus(currentUnit.GetComponent<CharLevel>().GetLevel())}";
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