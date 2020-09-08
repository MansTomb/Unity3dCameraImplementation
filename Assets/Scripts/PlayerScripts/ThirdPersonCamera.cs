using System;
using System.Numerics;
using UnityEngine;
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
    private float m_distance = 1.0f;

    private void Awake()
    {
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
            
            Debug.Log($"{m_distance}");
            Debug.DrawRay(transform.parent.position, transform.localPosition.normalized, Color.blue);
            if (Physics.Raycast(transform.parent.position, transform.localPosition.normalized, out hit, m_distance))
            {
                if (m_distance > 1.0f)
                {
                    transform.position = hit.point;
                    transform.Translate(transform.forward * Time.deltaTime);
                }
            }
            else
            {
                if (m_distance < 4.0f)
                {
                    transform.Translate(-transform.forward * Time.deltaTime);
                }
                else
                {
                    transform.Translate(transform.forward * Time.deltaTime);
                }
            }
            m_distance = Vector3.Distance(transform.parent.position, transform.localPosition);
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