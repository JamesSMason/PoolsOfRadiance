using PoR.Character.Customisation.Races;
using PoR.Character.Customisation.Statistics;
using System.Collections.Generic;
using UnityEngine;

namespace PoR.Character.Settings.Base
{
    public class BaseRace : MonoBehaviour
    {
        [SerializeField] private RaceSO raceSO = null;

        private string characterRace;
        private List<AbilityScore> abilityScoreList;
        private int speed;
        private Size size;

        private void Awake()
        {
            if (raceSO == null)
            {
                Debug.Log($"{gameObject.name} does not have a race Scriptable Object assigned).");
                return;
            }

            characterRace = raceSO.GetRace();
            abilityScoreList = raceSO.GetAbilityScoreList();
            speed = raceSO.GetSpeed();
            size = raceSO.GetSize();
        }

        public string GetCharacterRace()
        {
            return characterRace;
        }

        public List<AbilityScore> GetAbilityScoreList()
        {
            return abilityScoreList;
        }

        public Size GetSize()
        {
            return size;
        }

        public int GetSpeed()
        {
            return speed;
        }
    }
}