using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Win Condition")]
    public bool useEnemyCount = true;
    public string victorySceneName = "VictoryScene";

    [Header("References")]
    public GameObject victoryPanel;

    void Start()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    void Update()
    {
        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (!useEnemyCount) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (!string.IsNullOrEmpty(victorySceneName))
        {
            SceneManager.LoadScene(victorySceneName);
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
