using Por.Controls;
using PoR.Character;
using PoR.Character.Settings;
using PoR.Character.Settings.Base;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PoR.UI.Characterisations
{
    public class CharacterSheetUI : MonoBehaviour
    {
        [SerializeField] private Sprite isProficientImage = null;
        [SerializeField] private Sprite notProficientImage = null;
        [SerializeField] private TextMeshProUGUI[] abilityValuesText = new TextMeshProUGUI[6];
        [SerializeField] private TextMeshProUGUI[] abilityModifiersText = new TextMeshProUGUI[6];
        [SerializeField] private TextMeshProUGUI[] abilitySaveValuesText = new TextMeshProUGUI[6];
        [SerializeField] private Image[] saveProficiencyImage = new Image[6];
        [SerializeField] private TextMeshProUGUI ACText = null;

        private CharAbilityScores currentCharAbilityScores;
        private BaseClass baseClassDetails;

        private CharArmourClass charAC;

        private void UpdateSelectedUnit(Unit currentUnit)
        {
            if (currentUnit == null)
            {
                return;
            }

            currentCharAbilityScores = currentUnit.GetComponent<CharAbilityScores>();
            baseClassDetails = currentUnit.GetComponent<BaseClass>();

            charAC = currentUnit.GetComponent<CharArmourClass>();

            UpdateCharacterSheet();
        }

        private void UpdateCharacterSheet()
        {
            for (int i = 0; i < currentCharAbilityScores.GetAbilityScoresList().Count; i++)
            {
                abilityValuesText[i].text = currentCharAbilityScores.GetAbilityScoreText(i) ;

                abilityModifiersText[i].text = currentCharAbilityScores.GetAbilityScoreModifierText(i);

                abilitySaveValuesText[i].text = currentCharAbilityScores.GetAbilityScoreSavingThrowBonusText(i);

                saveProficiencyImage[i].sprite = baseClassDetails.GetIsSaveProficient(i) ? isProficientImage : notProficientImage;
            }

            ACText.text = charAC.GetArmourClass().ToString();
        }
    }
}