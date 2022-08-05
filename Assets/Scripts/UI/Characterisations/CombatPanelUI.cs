using Por.Controls;
using PoR.Character;
using PoR.Character.Customisation.Statistics;
using PoR.Character.Settings;
using PoR.Character.Settings.Base;
using System;
using TMPro;
using UnityEngine;

namespace PoR.UI.Characterisations
{
    public class CombatPanelUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI acText = null;
        [SerializeField] private TextMeshProUGUI initiativeText = null;
        [SerializeField] private TextMeshProUGUI speedText = null;
        [SerializeField] private TextMeshProUGUI currentHPText = null;
        [SerializeField] private TextMeshProUGUI tempHPText = null;
        [SerializeField] private TextMeshProUGUI hitDieText = null;

        private Unit currentUnit;

        private void OnEnable()
        {
            SelectedUnit.Instance.OnSelectedUnitChanged += SelectedUnit_OnSelectedUnitChanged;
            currentUnit = SelectedUnit.Instance.GetCurrentUnit();
            currentUnit.GetComponent<BaseClass>().OnLevelUp += BaseClass_OnLevelUp;
            UpdateFields();
        }

        private void OnDisable()
        {
            currentUnit.GetComponent<BaseClass>().OnLevelUp -= BaseClass_OnLevelUp;
            SelectedUnit.Instance.OnSelectedUnitChanged -= SelectedUnit_OnSelectedUnitChanged;
        }

        private void UpdateFields()
        {
            BaseRace baseRace = currentUnit.GetComponent<BaseRace>();
            BaseClass baseClass = currentUnit.GetComponent<BaseClass>();
            CharArmourClass charAC = currentUnit.GetComponent<CharArmourClass>();
            CharAbilityScores charAbilityScores = currentUnit.GetComponent<CharAbilityScores>();
            CharHealth charHealth = currentUnit.GetComponent<CharHealth>();

            acText.text = charAC.GetArmourClass().ToString();
            initiativeText.text = charAbilityScores.GetAbilityScoreModifier(Abilities.Dexterity).ToString();
            speedText.text = baseRace.GetSpeed().ToString();
            currentHPText.text = $"{charHealth.GetCurrentHP()} / {charHealth.GetMaxHP()}";
            tempHPText.text = $"{charHealth.GetTempHP()}";
            hitDieText.text = $"{baseClass.GetClassLevel()}d{baseClass.GetHitDie()}";
        }

        private void SelectedUnit_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            currentUnit = sender as Unit;
            if (currentUnit != null)
            {
                UpdateFields();
            }
        }

        private void BaseClass_OnLevelUp()
        {
            UpdateFields();
        }
    }
}