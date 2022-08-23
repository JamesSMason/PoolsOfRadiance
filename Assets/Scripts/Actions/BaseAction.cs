using PoR.Character;
using PoR.Grid;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoR.Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit unit;
        protected bool isActive = false;

        protected Action onActionComplete;

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>();
        }

        public abstract string GetActionName();

        public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

        public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            return GetValidActionGridPositionList().Contains(gridPosition);
        }

        public abstract List<GridPosition> GetValidActionGridPositionList();
    }
}