using System;
using UnityEngine;

namespace PoR.Character
{
    public class UnitSelectedVisual : MonoBehaviour
    {
        [SerializeField] private Unit unit = null;

        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            meshRenderer.enabled = (unit == UnitActionSystem.Instance.GetSelectedUnit());
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            UpdateVisual();
        }
    }
}