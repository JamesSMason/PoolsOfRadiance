using PoR.Character.Settings.Base;
using UnityEngine;

namespace PoR.Character.Settings
{
    public class CharHealth : MonoBehaviour
    {
        private BaseClass baseClass;

        private int maxHP;
        private int currentHP;
        private int tempHP;

        private void Awake()
        {
            baseClass = GetComponent<BaseClass>();
        }

        private void Start()
        {
            maxHP = baseClass.GetMaxHitPoints();
            tempHP = 0;
            currentHP = maxHP;
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
    }
}