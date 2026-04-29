using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxLife = 100;
    public int currentLife;

    void Start()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(int amount)
    {
        currentLife -= amount;

        if (currentLife <= 0)
        {
            currentLife = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
