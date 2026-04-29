using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI enemiesText;

    [Header("References")]
    public Health playerHealth;

    void Start()
    {
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
                playerHealth = player.GetComponent<Health>();
        }
    }

    void Update()
    {
        if (playerHealth != null && healthText != null)
        {
            healthText.text = "Vida: " + playerHealth.currentLife;
        }

        if (enemiesText != null)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemiesText.text = "Enemigos: " + enemies.Length;
        }
    }
}
