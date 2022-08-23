using PoR.Actions;
using PoR.Character;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace PoR.UI.Actions
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab = null;
        [SerializeField] private Transform actionButtonContainerTransform = null;

        private void Start()
        {
            CreateUnitActionButtons();
        }

        private void OnEnable()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        }

        private void OnDisable()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged -= UnitActionSystem_OnSelectedUnitChanged;
        }

        private void CreateUnitActionButtons()
        {
            foreach (Transform buttonTransform in actionButtonContainerTransform)
            {
                Destroy(buttonTransform.gameObject);
            }

            Unit unit = UnitActionSystem.Instance.GetSelectedUnit();

            foreach (BaseAction baseAction in unit.GetBaseActions())
            {
                Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
                ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
                actionButtonUI.SetBaseAction(baseAction);
            }
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            CreateUnitActionButtons();
        }
    }
}