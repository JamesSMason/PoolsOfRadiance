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
    public class CharacterSheetUI : MonoBehaviour
    {
        [SerializeField] private Sprite isProficientImage = null;
        [SerializeField] private Sprite notProficientImage = null;
        [SerializeField] private SelectedUnit selectedUnit;
        [SerializeField] private TextMeshProUGUI nameText = null;
        [SerializeField] private TextMeshProUGUI levelText = null;
        [SerializeField] private TextMeshProUGUI classText = null;
        [SerializeField] private TextMeshProUGUI raceText = null;
        [SerializeField] private TextMeshProUGUI[] abilityValuesText = new TextMeshProUGUI[6];
        [SerializeField] private TextMeshProUGUI[] abilityModifiersText = new TextMeshProUGUI[6];
        [SerializeField] private TextMeshProUGUI[] abilitySaveValuesText = new TextMeshProUGUI[6];
        [SerializeField] private Image[] saveProficiencyImage = new Image[6];
        [SerializeField] private TextMeshProUGUI ACText = null;

        private CharAbilityScores currentCharAbilityScores;
        private BasePersonal basePersonalDetails;
        private BaseRace baseRaceDetails;
        private BaseClass baseClassDetails;

        private CharLevel charLevel;
        private CharArmourClass charAC;

        private void OnEnable()
        {
            selectedUnit.OnSelectedUnitChanged += SelectedUnit_OnSelectedUnitChanged;
        }

        private void OnDisable()
        {
            selectedUnit.OnSelectedUnitChanged -= SelectedUnit_OnSelectedUnitChanged;
        }

        private void UpdateSelectedUnit(Unit currentUnit)
        {
            if (currentUnit == null)
            {
                return;
            }

            currentCharAbilityScores = currentUnit.GetComponent<CharAbilityScores>();
            basePersonalDetails = currentUnit.GetComponent<BasePersonal>();
            baseRaceDetails = currentUnit.GetComponent<BaseRace>();
            baseClassDetails = currentUnit.GetComponent<BaseClass>();

            charLevel = currentUnit.GetComponent<CharLevel>();
            charAC = currentUnit.GetComponent<CharArmourClass>();

            UpdateCharacterSheet();
        }

        private void UpdateCharacterSheet()
        {
            List<AbilityScore> abilityScoreList = currentCharAbilityScores.GetAbilityScoresList();
            List<int> abilityScoreModifiersList = currentCharAbilityScores.GetAbilityScoreModifiersList();
            List<int> abilityScoreSavingThrowBonusList = currentCharAbilityScores.GetAbilityScoreSavingThrowBonusList();

            nameText.text = basePersonalDetails.GetCharacterName();
            levelText.text = charLevel.GetLevel().ToString();
            classText.text = baseClassDetails.GetCharacterClass();
            raceText.text = baseRaceDetails.GetCharacterRace();

            for (int i = 0; i < abilityScoreList.Count; i++)
            {
                abilityValuesText[i].text = abilityScoreList[i].GetValue().ToString();
                if (abilityScoreModifiersList[i] > 0)
                {
                    abilityModifiersText[i].text = $"+{abilityScoreModifiersList[i]}";
                }
                else
                {
                    abilityModifiersText[i].text = abilityScoreModifiersList[i].ToString();
                }
                if (abilityScoreSavingThrowBonusList[i] > 0)
                {
                    abilitySaveValuesText[i].text = $"+{abilityScoreSavingThrowBonusList[i]}";
                }
                else
                {
                    abilitySaveValuesText[i].text = abilityScoreSavingThrowBonusList[i].ToString();
                }
                if (baseClassDetails.GetIsSaveProficient(abilityScoreList[i].GetAbility()))
                {
                    saveProficiencyImage[i].sprite = isProficientImage;
                }
                else
                {
                    saveProficiencyImage[i].sprite = notProficientImage;
                }
            }

            ACText.text = charAC.GetArmourClass().ToString();
        }

        private void SelectedUnit_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            UpdateSelectedUnit(sender as Unit);
        }
    }
}