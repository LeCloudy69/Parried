using UnityEngine;
using UnityEngine.SceneManagement; // Needed to reload scene

public class GameFlow : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverPanel;
    public GameObject victoryPanel;

    // Call this when Player Health hits 0
    public void TriggerGameOver()
    {
        ShowCursor();
        if(gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    // Call this when WaveManager finishes
    public void TriggerVictory()
    {
        ShowCursor();
        if(victoryPanel != null) victoryPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    // Call this from the "Restart" button
    public void RestartGame()
    {
        Time.timeScale = 1f; 
        // Reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}