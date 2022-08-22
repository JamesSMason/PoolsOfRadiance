using PoR.Character;
using PoR.Grid;
using System.Collections.Generic;
using UnityEngine;

namespace PoR.Actions
{
    public class MoveAction : MonoBehaviour
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
        private Unit unit;

        private void Awake()
        {
            unit = GetComponent<Unit>();
            targetPosition = transform.position;
        }

        void Update()
        {
            if (Vector3.Distance(targetPosition, transform.position) > stoppingDistance)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.position += moveDir * moveSpeed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
                unitAnimator.SetBool("IsWalking", true);
            }
            else
            {
                unitAnimator.SetBool("IsWalking", false);
            }
        }

        public void Move(GridPosition gridPosition)
        {
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