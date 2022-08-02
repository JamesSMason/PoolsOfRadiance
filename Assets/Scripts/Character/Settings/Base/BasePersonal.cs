using UnityEngine;

namespace PoR.Character.Settings.Base
{
    public class BasePersonal : MonoBehaviour
    {
        [SerializeField] string characterName;
        [SerializeField] string gender;
        [SerializeField] string age;
        [SerializeField] string height;
        [SerializeField] string weight;
        [SerializeField] string hairColour;
        [SerializeField] string eyeColour;
        [SerializeField] string skinColour;
        [SerializeField] string backgroundDetails;

        public string GetCharacterName()
        { 
            return characterName;
        }

        public string GetCharacterGender()
        {
            return gender;
        }

        public string GetCharacterAge()
        {
            return age;
        }

        public string GetCharacterHeight()
        {
            return height;
        }

        public string GetCharacterWeight()
        {
            return weight;
        }

        public string GetCharacterHairColour()
        {
            return hairColour;
        }

        public string GetCharacterEyeColour()
        {
            return eyeColour;
        }

        public string GetCharacterSkinColour()
        {
            return skinColour;
        }

        public string GetCharacterBackground()
        {
            return backgroundDetails;
        }
    }
}