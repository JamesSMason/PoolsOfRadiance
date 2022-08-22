using PoR.Character;
using System;
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
    }
}