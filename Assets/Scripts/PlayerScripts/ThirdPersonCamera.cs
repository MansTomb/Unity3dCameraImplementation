using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public float cameraSpeed = 3f;
    public float distance = 3f;

    private Vector3 m_inputCoordinates = Vector3.zero;
    private Vector3 m_offset;

    private float m_mouseX;
    private float m_mouseY;

    private float m_distance;

    private void Awake()
    {
        m_offset = transform.localPosition.normalized;
        m_distance = transform.localPosition.magnitude;
    }

    private void Update()
    {
        CameraControl();
    }

    private void CameraControl()
    {
        m_mouseX += m_inputCoordinates.x * cameraSpeed;
        m_mouseY -= m_inputCoordinates.y * cameraSpeed;
        m_mouseY = Mathf.Clamp(m_mouseY, -50, 35);
        RaycastHit hit;
        
        if (Physics.Raycast(transform.parent.position, transform.position - player.position, out hit) &&
            !hit.collider.gameObject.CompareTag("Player"))
        {
            m_distance = Mathf.Clamp(hit.distance, 1.0f, distance);
        }
        else
        {
            m_distance = distance;
        }
        transform.LookAt(player);
        transform.localPosition = Vector3.Lerp(transform.localPosition, m_offset * m_distance, Time.deltaTime * 2);
        player.rotation = Quaternion.Euler(m_mouseY, m_mouseX, 0);
    }

    private void OnCamera(InputValue value)
    {
        Vector2 vectorValue = value.Get<Vector2>();
        m_inputCoordinates = new Vector3(vectorValue.x, vectorValue.y, 0);
    }
}