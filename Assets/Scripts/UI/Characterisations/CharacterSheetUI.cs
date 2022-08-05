using PoR.Character.Settings;
using UnityEngine;

namespace PoR.UI.Characterisations
{
    public class CharacterSheetUI : MonoBehaviour
    {
        [SerializeField] private Sprite isProficientImage = null;
        [SerializeField] private Sprite notProficientImage = null;

        private CharArmourClass charAC;

        public Sprite GetProficiencyImage(bool isProficient)
        {
            if (isProficient)
            {
                return isProficientImage;
            }
            return notProficientImage;
        }
    }
}