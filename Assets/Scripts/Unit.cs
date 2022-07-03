using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float stoppingDistance = 0.2f;

    private Vector3 targetPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Move(new Vector3(6, 0, 6));
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