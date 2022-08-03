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
        [SerializeField] private TextMeshProUGUI ACText = null;

        private CharArmourClass charAC;

        public Sprite GetProficiencyImage(bool isProficient)
        {
            if (isProficient)
            {
                return isProficientImage;
            }
            return notProficientImage;
        }

        private void UpdateSelectedUnit(Unit currentUnit)
        {
            if (currentUnit == null)
            {
                return;
            }

            charAC = currentUnit.GetComponent<CharArmourClass>();

            UpdateCharacterSheet();
        }

        private void UpdateCharacterSheet()
        {

            ACText.text = charAC.GetArmourClass().ToString();
        }
    }
}