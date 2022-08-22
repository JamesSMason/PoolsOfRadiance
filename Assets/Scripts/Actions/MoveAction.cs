using PoR.Character;
using PoR.Controls;
using PoR.Grid;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoR.Actions
{
    public class MoveAction : BaseAction
    {
        [Header("Component References")]
        [SerializeField] private Animator unitAnimator = null;

        [Header("Movement Variables")]
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float stoppingDistance = 0.2f;
        [SerializeField] private float rotationSpeed = 100f;
        // TODO: Replace with speed
        [SerializeField] private int maxMoveDistance = 4;

        private Vector3 targetPosition;

        protected override void Awake()
        {
            base.Awake();
            targetPosition = transform.position;
        }

        void Update()
        {
            if (!isActive)
            {
                return;
            }

            Vector3 moveDir = (targetPosition - transform.position).normalized;

            if (Vector3.Distance(targetPosition, transform.position) > stoppingDistance)
            {
                transform.position += moveDir * moveSpeed * Time.deltaTime;
                unitAnimator.SetBool("IsWalking", true);
            }
            else
            {
                unitAnimator.SetBool("IsWalking", false);
                isActive = false;
                onActionComplete();
            }
                
            transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        }

        public void Move(GridPosition gridPosition, Action onActionComplete)
        {
            this.onActionComplete = onActionComplete;
            isActive = true;
            targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        }

        public bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            return GetValidActionGridPositionList().Contains(gridPosition);
        }

        public List<GridPosition> GetValidActionGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();

            GridPosition unitGridPostion = unit.GetGridPosition();
            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPostion + offsetGridPosition;
                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        continue;
                    }

                    if (unitGridPostion == testGridPosition)
                    {
                        continue;
                    }

                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    {
                        continue;
                    }

                    validGridPositionList.Add(testGridPosition);
                    Debug.Log(testGridPosition.ToString());
                }
            }

            return validGridPositionList;
        }
    }
}