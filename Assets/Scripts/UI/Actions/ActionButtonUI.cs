using PoR.Actions;
using PoR.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PoR.UI.Actions
{
    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro = null;
        [SerializeField] private Button button = null;

        public void SetBaseAction(BaseAction baseAction)
        {
            textMeshPro.text = baseAction.GetActionName().ToUpper();
            button.onClick.AddListener( () => 
            { 
                UnitActionSystem.Instance.SetSelectedAction(baseAction); 
            } );
        }
    }
}