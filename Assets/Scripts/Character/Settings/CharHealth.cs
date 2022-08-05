using PoR.Character.Customisation.Statistics;
using PoR.Character.Settings.Base;
using System.Collections.Generic;
using UnityEngine;

namespace PoR.Character.Settings
{
    public class CharHealth : MonoBehaviour
    {
        private BaseClass baseClass;
        private CharAbilityScores charAbilityScores;

        private List<int> hitPointsPerLevel = new List<int>();
        private int maxHP = 0;
        private int currentHP = 0;
        private int tempHP = 0;

        private void Awake()
        {
            baseClass = GetComponent<BaseClass>();
            charAbilityScores = GetComponent<CharAbilityScores>();
        }

        private void Start()
        {
            hitPointsPerLevel.Add(baseClass.GetHitDie());
            tempHP = 0;
        }

        private void OnEnable()
        {
            if (charAbilityScores != null)
            {
                charAbilityScores.OnAbilitiesChanged += CharAbilityScores_OnAbilitiesChanged;
            }
            if (baseClass != null)
            {
                baseClass.OnLevelUp += BaseClass_OnLevelUp;
            }
        }

        private void OnDisable()
        {
            if (charAbilityScores != null)
            {
                charAbilityScores.OnAbilitiesChanged -= CharAbilityScores_OnAbilitiesChanged;
            }
            if (baseClass != null)
            {
                baseClass.OnLevelUp += BaseClass_OnLevelUp;
            }
        }

        public int GetMaxHP()
        {
            return maxHP;
        }

        public int GetCurrentHP()
        {
            return currentHP;
        }

        public int GetTempHP()
        {
            return tempHP;
        }

        public void IncrementLevel()
        {
            hitPointsPerLevel.Add(Random.Range(1, baseClass.GetHitDie() + 1));
            SetMaxHitPoints();
        }

        private void SetMaxHitPoints()
        {
            int hpLost = maxHP - currentHP;
            maxHP = 0;
            for (int i = 0; i < hitPointsPerLevel.Count; i++)
            {
                maxHP += hitPointsPerLevel[i];
            }
            // TODO: Assumes single class
            maxHP += charAbilityScores.GetAbilityScoreModifier(Abilities.Constitution) * baseClass.GetClassLevel();
            currentHP = maxHP - hpLost;
            Debug.Log($"New HP = {currentHP} / {maxHP}");
        }

        private void CharAbilityScores_OnAbilitiesChanged(object sender, System.EventArgs e)
        {
            SetMaxHitPoints();
        }

        private void BaseClass_OnLevelUp()
        {
            IncrementLevel();
        }
    }
}