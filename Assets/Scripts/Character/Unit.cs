using PoR.Actions;
using PoR.Controls;
using PoR.Grid;
using UnityEngine;

namespace PoR.Character
{
    public class Unit : MonoBehaviour
    {
        private GridPosition gridPosition;
        private MoveAction moveAction;
        private SpinAction spinAction;
        private BaseAction[] baseActionArray;

        private void Awake()
        {
            moveAction = GetComponent<MoveAction>();
            spinAction = GetComponent<SpinAction>();
            baseActionArray = GetComponents<BaseAction>();
        }

        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        }

        private void Update()
        {
            GridPosition newGridPosition =  LevelGrid.Instance.GetGridPosition(transform.position);
            if (newGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
                gridPosition = newGridPosition;
            }
        }

        public MoveAction GetMoveAction()
        {
            return moveAction;
        }

        public SpinAction GetSpinAction()
        {
            return spinAction;
        }

        public GridPosition GetGridPosition()
        {
            return gridPosition;
        }

        public BaseAction[] GetBaseActions()
        {
            return baseActionArray;
        }
    }
}