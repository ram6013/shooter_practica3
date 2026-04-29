using UnityEngine;
using UnityEngine.InputSystem;

public class GunAim : MonoBehaviour
{
    [Header("Positions")]
    public Transform hipPosition;
    public Transform aimPosition;

    [Header("Settings")]
    public float aimSpeed = 10f;
    public float aimFov = 50f;
    public float normalFov = 60f;
    public float aimSensitivityMultiplier = 0.7f;

    [Header("References")]
    public Camera playerCamera;
    public PlayerControllerFPS playerController;
    public GameObject shootArrow;

    public bool isAiming { get; private set; }
    private float originalSensitivity;

    void Start()
    {
        if (playerController != null)
            originalSensitivity = playerController.mouseSensitivity;
        else
            originalSensitivity = 0.2f;

        if (shootArrow != null)
            shootArrow.SetActive(false);
    }

    void Update()
    {
        isAiming = Mouse.current != null && Mouse.current.rightButton.isPressed;

        if (shootArrow != null)
            shootArrow.SetActive(isAiming);

        Transform target = isAiming ? aimPosition : hipPosition;
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * aimSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * aimSpeed);
        }

        if (playerCamera != null)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, isAiming ? aimFov : normalFov, Time.deltaTime * aimSpeed);
        }

        if (playerController != null && originalSensitivity > 0)
        {
            playerController.mouseSensitivity = isAiming ? originalSensitivity * aimSensitivityMultiplier : originalSensitivity;
        }
    }
}
