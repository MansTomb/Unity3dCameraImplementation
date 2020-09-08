using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class ThirdPersonCamera : MonoBehaviour
{
    [FormerlySerializedAs("Player")] public Transform player;
    public float cameraSpeed = 3;

    private Vector3 m_currentCoordinates = Vector3.zero;

    private void Update()
    {
        CameraControl();
    }

    private void CameraControl()
    {
        if (m_currentCoordinates != Vector3.zero)
        {
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