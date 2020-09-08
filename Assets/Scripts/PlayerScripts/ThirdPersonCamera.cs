using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public float cameraSpeed = 3;

    private Vector3 m_currentCoordinates = Vector3.zero;
    private Vector3 m_futureLocation = Vector3.zero;
    private float m_distance = 1.0f;

    private void Update()
    {
        CameraControl();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(m_futureLocation, 0.3f);
        Debug.DrawRay(transform.parent.position, m_futureLocation, Color.blue);
    }

    private void CameraControl()
    {
        if (m_currentCoordinates != Vector3.zero)
        {
            m_futureLocation = transform.position + m_currentCoordinates.normalized * (Time.deltaTime * cameraSpeed);
            RaycastHit hit;

            if (Physics.Raycast(transform.parent.position, m_futureLocation, out hit))
            {
                m_distance = Mathf.Clamp(hit.distance, 1.0f, 3.0f);
            }
            else
            {
                m_distance = 3.0f;
            }
            transform.Translate(m_currentCoordinates.normalized * (Time.deltaTime * cameraSpeed));
            transform.LookAt(player.position);
        }
    }

    private void OnCamera(InputValue value)
    {
        Vector2 vectorValue = value.Get<Vector2>();
        m_currentCoordinates = new Vector3(vectorValue.x, vectorValue.y, 0);
    }
}