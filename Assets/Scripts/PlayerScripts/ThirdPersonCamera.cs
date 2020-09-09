using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public float cameraSpeed = 3;

    private Vector3 m_currentCoordinates = Vector3.zero;
    private Vector3 m_offset;

    private float m_offsetX = 0f;
    private float m_offsetY = 1f;
    private float m_offsetZ = -3f;

    private float m_mouseX, m_mouseY;

    private float m_distance;

    private void Awake()
    {
        m_offset = new Vector3(player.position.x + m_offsetX, player.position.y + m_offsetY, player.position.z + m_offsetZ);
        m_distance = transform.localPosition.magnitude;
    }

    private void Update()
    {
        CameraControl();
    }

    private void CameraControl()
    {
        m_mouseX += m_currentCoordinates.x * cameraSpeed;
        m_mouseY -= m_currentCoordinates.y * cameraSpeed;
        m_mouseY = Mathf.Clamp(m_mouseY, -50, 35);
        RaycastHit hit;
        
        if (Physics.Raycast(transform.parent.position, transform.position - player.position, out hit) &&
            !hit.collider.gameObject.CompareTag("Player"))
        {
            m_distance = Mathf.Clamp(hit.distance, 1.0f, 3.0f);
        }
        else
        {
            m_distance = 3.0f;
        }
        transform.LookAt(player);
        transform.localPosition = Vector3.Lerp(transform.localPosition, m_offset.normalized * m_distance, Time.deltaTime * 2);
        player.rotation = Quaternion.Euler(m_mouseY, m_mouseX, 0);
    }

    private void OnCamera(InputValue value)
    {
        Vector2 vectorValue = value.Get<Vector2>();
        m_currentCoordinates = new Vector3(vectorValue.x, vectorValue.y, 0);
    }
}