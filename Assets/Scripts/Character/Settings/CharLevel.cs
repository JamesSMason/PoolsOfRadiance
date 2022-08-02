using PoR.Character.Settings.Base;
using UnityEngine;

namespace PoR.Character.Settings
{
    public class CharLevel : MonoBehaviour
    {
        private int level;
        private BaseClass[] baseClasses;

        private void Start()
        {
            baseClasses = GetComponents<BaseClass>();
            CalculateLevel();
        }
        private void CalculateLevel()
        {
            level = 0;
            foreach (BaseClass unitClass in baseClasses)
            {
                level += unitClass.GetClassLevel();
            }
        }

        public int GetLevel()
        {
            return level;
        }
    }
}