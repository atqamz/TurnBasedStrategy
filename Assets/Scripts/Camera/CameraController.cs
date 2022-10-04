using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;

    private CinemachineTransposer cinemachineTransposer;
    private Vector3 targetFollowOffset;

    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        HandleCameraMoving();
        HandleCameraRotation();
        HandleCameraZoom();

    }

    private void HandleCameraMoving()
    {
        Vector3 inputMoveDirection = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDirection.z = +1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDirection.z = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDirection.x = +1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDirection.x = -1f;
        }

        float moveSpeed = 10f;
        Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraRotation()
    {
        Vector3 inputRotateDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            inputRotateDirection.y = +1f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            inputRotateDirection.y = -1f;
        }

        float rotateSpeed = 100f;
        transform.eulerAngles += inputRotateDirection * rotateSpeed * Time.deltaTime;
    }

    private void HandleCameraZoom()
    {
        float zoomAmount = 1f;
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomAmount;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomAmount;
        }
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);

        float zoomSpeed = 5f;
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, zoomSpeed * Time.deltaTime);
    }
}
