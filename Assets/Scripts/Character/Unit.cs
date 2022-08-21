using PoR.Grid;
using UnityEngine;

namespace PoR.Character
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float stoppingDistance = 0.2f;
        [SerializeField] private float rotationSpeed = 100f;
        [SerializeField] private Animator unitAnimator = null;

        private Vector3 targetPosition;
        private GridPosition gridPosition;

        private void Awake()
        {
            targetPosition = transform.position;
        }

        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        }

        private void Update()
        {
            if (Vector3.Distance(targetPosition, transform.position) > stoppingDistance)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
                transform.position += moveDir * moveSpeed * Time.deltaTime;
                unitAnimator.SetBool("IsWalking", true);
            }
            else
            {
                unitAnimator.SetBool("IsWalking", false);
            }

            GridPosition oldGridPosition = gridPosition;
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if (oldGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMovedGridPosition(this, oldGridPosition, gridPosition);
            }
        }

        public void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }
    }
}