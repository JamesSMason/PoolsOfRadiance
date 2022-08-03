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
    public class CombatPanelUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText = null;
        [SerializeField] private RawImage portraitImage = null;
        [SerializeField] private TextMeshProUGUI levelText = null;
        [SerializeField] private TextMeshProUGUI classText = null;
        [SerializeField] private TextMeshProUGUI raceText = null;
        [SerializeField] private TextMeshProUGUI alignmentText = null;
        [SerializeField] private TextMeshProUGUI xpText = null;
        [SerializeField] private TextMeshProUGUI backgroundText = null;
        [SerializeField] private TextMeshProUGUI ageText = null;
        [SerializeField] private TextMeshProUGUI heightText = null;
        [SerializeField] private TextMeshProUGUI weightText = null;
        [SerializeField] private TextMeshProUGUI eyesText = null;
        [SerializeField] private TextMeshProUGUI skinText = null;
        [SerializeField] private TextMeshProUGUI hairText = null;

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
            CharPersonal charPersonal = currentUnit.GetComponent<CharPersonal>();

            nameText.text = charPersonal.GetCharacterName();
            levelText.text = currentUnit.GetComponent<CharLevel>().GetLevel().ToString();
            classText.text = currentUnit.GetComponent<BaseClass>().GetCharacterClass();
            raceText.text = currentUnit.GetComponent<BaseRace>().GetCharacterRace();
            alignmentText.text = charPersonal.GetCharacterAlignment();
            // TODO: Generate personal texture on character creation
            portraitImage.texture = charPersonal.GetCharacterPortrait();
            // TODO: XP
            xpText.text = "0";
            backgroundText.text = charPersonal.GetCharacterBackground();
            ageText.text = charPersonal.GetCharacterAge();
            heightText.text = charPersonal.GetCharacterHeight();
            weightText.text = charPersonal.GetCharacterWeight();
            eyesText.text = charPersonal.GetCharacterEyeColour();
            skinText.text = charPersonal.GetCharacterSkinColour();
            hairText.text = charPersonal.GetCharacterHairColour();
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