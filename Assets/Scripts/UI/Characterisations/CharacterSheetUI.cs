using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSheetUI : MonoBehaviour
{
    [SerializeField] private SelectedUnit selectedUnit;
    [SerializeField] TextMeshProUGUI characterName = null;
    [SerializeField] TextMeshProUGUI[] abilityValues = null;
    [SerializeField] TextMeshProUGUI[] abilityModifiers = null;
    [SerializeField] TextMeshProUGUI[] abilitySaveValues = null;

    private Unit currentUnit;
    private DerivedStats currentUnitsDerivedStats;

    private void OnEnable()
    {
        selectedUnit.OnSelectedUnitChanged += SelectedUnit_OnSelectedUnitChanged;
    }

    private void OnDisable()
    {
        selectedUnit.OnSelectedUnitChanged -= SelectedUnit_OnSelectedUnitChanged;
    }

    private void UpdateCharacterSheet()
    {
        List<AbilityScore> abilityScoreList = currentUnitsDerivedStats.GetAbilityScoresList();
        List<int> abilityScoreModifiersList = currentUnitsDerivedStats.GetAbilityScoreModifiersList();

        characterName.text = currentUnitsDerivedStats.GetCharacterName();

        for (int i = 0; i < abilityScoreList.Count; i++)
        {
            abilityValues[i].text = abilityScoreList[i].GetValue().ToString();
            abilityModifiers[i].text = abilityScoreModifiersList[i].ToString();
        }
    }

    private void SelectedUnit_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        currentUnit = sender as Unit;
        if (currentUnit.TryGetComponent<DerivedStats>(out currentUnitsDerivedStats))
        {
            UpdateCharacterSheet();
        }
    }
}