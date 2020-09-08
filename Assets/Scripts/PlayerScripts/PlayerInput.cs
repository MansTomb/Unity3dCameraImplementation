using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public float movementSpeed = 3;
    public Camera playerCamera;
    
    private CharacterController m_controller;
    private Vector3 m_movementInput;
    
    private void Awake() {
        m_controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        m_controller.Move(m_movementInput * (movementSpeed * Time.deltaTime));
    }
    
    private void OnMove(InputValue value)
    {
        Vector2 newInputMovement = value.Get<Vector2>();
        m_movementInput = new Vector3(newInputMovement.x, 0.0f, newInputMovement.y);
        m_movementInput = Quaternion.Euler(0.0f, playerCamera.transform.eulerAngles.y, 0.0f) * m_movementInput;
    }
}
