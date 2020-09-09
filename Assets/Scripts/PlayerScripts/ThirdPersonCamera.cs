using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public float cameraSpeed = 3;

    private Vector3 m_currentCoordinates = Vector3.zero;
    private Vector3 m_offset;
    private float m_distance = 1.0f;

    private float m_offsetX = 0f;
    private float m_offsetY = 1f;
    private float m_offsetZ = -3f;

    private void Awake()
    {
        m_offset = new Vector3(player.position.x + m_offsetX, player.position.y + m_offsetY, player.position.z + m_offsetZ);
        transform.rotation = Quaternion.Euler(15, 0, 0);
        m_distance = Vector3.Distance(transform.parent.position, transform.position);
    }

    private void Update()
    {
        CameraControl();
    }

    private void CameraControl()
    {
        if (m_currentCoordinates != Vector3.zero)
        {
            RaycastHit hit;
            
            Debug.DrawRay(transform.parent.position, transform.localPosition.normalized, Color.blue);
            if (Physics.Raycast(transform.parent.position, transform.localPosition.normalized, out hit, m_distance) &&
                !hit.collider.gameObject.CompareTag("Player"))
            {
                m_offset += (transform.forward * (Time.deltaTime * 5));
            }
            m_offset = Quaternion.AngleAxis(m_currentCoordinates.normalized.x * cameraSpeed * Time.deltaTime, Vector3.up) *
                       Quaternion.AngleAxis(m_currentCoordinates.normalized.y * cameraSpeed * Time.deltaTime, Vector3.right) * m_offset;
        }
        if (m_distance < 4.0f)
        {
            m_offset += (-transform.forward * (Time.deltaTime));
        }
        else
        {
            m_offset += (transform.forward * (Time.deltaTime));
        }
        m_distance = Vector3.Distance(transform.parent.position, transform.position);
        transform.position = player.position + m_offset;
        transform.LookAt(player);
    }

    private void OnCamera(InputValue value)
    {
        Vector2 vectorValue = value.Get<Vector2>();
        m_currentCoordinates = new Vector3(vectorValue.x, vectorValue.y, 0);
    }
}