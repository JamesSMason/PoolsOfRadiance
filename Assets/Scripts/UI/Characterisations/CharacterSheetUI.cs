using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSheetUI : MonoBehaviour
{
    [SerializeField] DerivedStats derivedStats = null;
    [SerializeField] TextMeshProUGUI characterName = null;
    [SerializeField] TextMeshProUGUI[] abilityValues = null;
    [SerializeField] TextMeshProUGUI[] abilityModifiers = null;
    [SerializeField] TextMeshProUGUI[] abilitySaveValues = null;

    // TODO: This will be updated on unit selection

    private void OnEnable()
    {
        derivedStats.OnAbilitiesChanged += DerivedStats_OnAbilitiesChanged;
    }

    private void OnDisable()
    {
        derivedStats.OnAbilitiesChanged -= DerivedStats_OnAbilitiesChanged;
    }

    private void DerivedStats_OnAbilitiesChanged(object sender, EventArgs e)
    {
        List<AbilityScore> abilityScoreList = derivedStats.GetAbilityScoresList();
        List<int> abilityScoreModifiersList = derivedStats.GetAbilityScoreModifiersList();

        characterName.text = derivedStats.GetCharacterName();

        for (int i = 0; i < abilityScoreList.Count; i++)
        {
            abilityValues[i].text = abilityScoreList[i].GetValue().ToString();
            abilityModifiers[i].text = abilityScoreModifiersList[i].ToString();
        }
    }
}