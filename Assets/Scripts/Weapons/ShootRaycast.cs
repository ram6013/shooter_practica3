using UnityEngine;
using UnityEngine.InputSystem;

public class ShootRaycast : MonoBehaviour
{
    [Header("Shoot Settings")]
    public float fireRate = 0.2f;
    public Transform firePoint;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;

    [Header("References")]
    public Camera playerCamera;
    public GunAim gunAim;

    private float nextFireTime = 0f;


    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        if (Time.time < nextFireTime)
            return;

        if (playerCamera == null || projectilePrefab == null)
        {
            Debug.LogError("Player Camera or Projectile Prefab not assigned to ShootRaycast!");
            return;
        }

        nextFireTime = Time.time + fireRate;

        if (gunAim != null && !gunAim.isAiming)
            return;

        Vector3 spawnPos = firePoint != null ? firePoint.position : playerCamera.transform.position;
        Quaternion spawnRot = playerCamera.transform.rotation;

        GameObject proj = Instantiate(projectilePrefab, spawnPos, spawnRot);
        Projectile projScript = proj.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.speed = projectileSpeed;
        }
    }
}
