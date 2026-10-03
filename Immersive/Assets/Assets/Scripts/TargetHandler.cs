using UnityEngine;
using UnityEngine.InputSystem;

public class TargetRailController : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("World Z Limits")]
    public float closestZ = 7.3f;
    public float farthestZ = 16.7f;

    [Header("Movement")]
    public float moveSpeed = 0.3f;

    [Header("VR Buttons")]
    public InputActionReference moveForward;
    public InputActionReference moveBackward;

    private bool playerInRange;

    void OnEnable()
    {
        moveForward.action.Enable();
        moveBackward.action.Enable();
    }

    void OnDisable()
    {
        moveForward.action.Disable();
        moveBackward.action.Disable();
    }

    void Update()
    {
        if (!playerInRange)
            return;

        float movement = 0f;

        if (moveForward.action.IsPressed())
            movement = 1f;

        if (moveBackward.action.IsPressed())
            movement = -1f;

        if (movement == 0f)
            return;

        Vector3 newPosition = target.position;

        newPosition.z += movement * moveSpeed * Time.deltaTime;

        newPosition.z = Mathf.Clamp(
            newPosition.z,
            closestZ,
            farthestZ
        );

        target.position = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
