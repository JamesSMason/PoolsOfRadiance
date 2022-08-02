using UnityEngine;

namespace PoR.Character.Settings
{
    public class CharArmourClass : MonoBehaviour
    {
        private int armourClass;

        private void Start()
        {
            OnEquipmentChanged();
        }

        public int GetArmourClass()
        {
            return armourClass;
        }

        // TODO: Implement event for changed equipment and update ac from it.
        private void OnEquipmentChanged()
        {
            armourClass = 10;
        }
    }
}