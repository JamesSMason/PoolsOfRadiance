using UnityEngine;

namespace PoR.UI
{
    public class ToggleUI : MonoBehaviour
    {
        [SerializeField] KeyCode UIToggleKey;
        [SerializeField] GameObject UIPanelToToggle;

        private void Update()
        {
            if (Input.GetKeyDown(UIToggleKey))
            {
                CloseUIWindows(!UIPanelToToggle.activeSelf);
            }
        }

        private void CloseUIWindows(bool turnPanelOn)
        {
            foreach (Transform childPanel in GetComponentInChildren<Transform>())
            {
                childPanel.gameObject.SetActive(false);
            }
            if (turnPanelOn)
            {
                UIPanelToToggle.SetActive(turnPanelOn);
            }
        }
    }
}