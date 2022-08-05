using PoR.Character.Customisation.Statistics;
using System;
using UnityEngine;

namespace PoR.Character.Settings
{
    public class CharArmourClass : MonoBehaviour
    {
        [SerializeField] private CharAbilityScores charAbilityScores = null;

        private int armourClass;

        #region Unity Messages

        private void OnEnable()
        {
            if (charAbilityScores != null)
            {
                charAbilityScores.OnAbilitiesChanged += CharAbilityScores_OnAbilitiesChanged;
            }
        }

        #endregion

        #region Public Methods

        public int GetArmourClass()
        {
            return armourClass;
        }

        #endregion

        #region Private Methods

        // TODO: Implement event for changed equipment and update ac from it.
        // TODO: Implement armour proficiency
        private void OnEquipmentChanged()
        {
            armourClass = 10 + charAbilityScores.GetAbilityScoreModifier(Abilities.Dexterity);
        }


        private void CharAbilityScores_OnAbilitiesChanged(object sender, EventArgs e)
        {
            OnEquipmentChanged();
        }

        #endregion
    }
}