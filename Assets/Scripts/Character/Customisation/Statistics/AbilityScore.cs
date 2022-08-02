using System;
using UnityEngine;

namespace PoR.Character.Customisation.Statistics
{
    [Serializable]
    public class AbilityScore
    {
        [SerializeField] private Abilities ability;
        [SerializeField] private int value;

        public Abilities GetAbility()
        {
            return ability;
        }

        public void SetAbility(Abilities ability)
        {
            this.ability = ability;
        }

        public int GetValue()
        {
            return value;
        }

        public void SetValue(int value)
        {
            this.value = value;
        }
    }
}