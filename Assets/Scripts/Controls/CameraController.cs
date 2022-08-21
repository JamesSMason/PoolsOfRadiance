using Cinemachine;
using UnityEngine;

namespace Por.Controls
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera = null;
        [SerializeField] private float cameraMoveSpeed = 10f;
        [SerializeField] private float cameraRotationSpeed = 100f;
        [SerializeField] private float zoomFactor = 1f;
        [SerializeField] private float zoomSpeed = 5f;

        private CinemachineTransposer cinemachineTransposer;
        private Vector3 targetFollowOffset;

        private const float MIN_FOLLOW_Y_OFFSET = 2f;
        private const float MAX_FOLLOW_Y_OFFSET = 12f;

        private void Start()
        {
            cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            targetFollowOffset = cinemachineTransposer.m_FollowOffset;
        }

        void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleZoom();
        }

        private void HandleMovement()
        {
            Vector3 inputMoveDir = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                inputMoveDir.z = 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputMoveDir.x = -1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputMoveDir.z = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputMoveDir.x = 1;
            }

            Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
            transform.position += moveVector * cameraMoveSpeed * Time.deltaTime;
        }

        private void HandleRotation()
        {
            Vector3 inputRotationVector = Vector3.zero;

            if (Input.GetKey(KeyCode.Q))
            {
                inputRotationVector.y = 1;
            }
            if (Input.GetKey(KeyCode.E))
            {
                inputRotationVector.y = -1;
            }

            transform.eulerAngles += inputRotationVector * cameraRotationSpeed * Time.deltaTime;
        }

        private void HandleZoom()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                targetFollowOffset.y -= zoomFactor;
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                targetFollowOffset.y += zoomFactor;
            }

            targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
            cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
        }
    }
}