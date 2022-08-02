using Por.Core;
using UnityEngine;

namespace PoR.Character
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float stoppingDistance = 0.2f;

        private Vector3 targetPosition;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Move(MouseWorld.GetPosition());
            }

            if (Vector3.Distance(targetPosition, transform.position) > stoppingDistance)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.position += moveDir * moveSpeed * Time.deltaTime;
            }
        }

        private void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }
    }
}