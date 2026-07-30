using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;

    private void OnEnable()
    {
        PlayerLocomotion.OnPlayerDied += ShowGameOver;
    }

    private void OnDisable()
    {
        PlayerLocomotion.OnPlayerDied -= ShowGameOver;
    }

    private void ShowGameOver()
    {
        _gameOverPanel.SetActive(true);
    }

    public void OnRestartClicked()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
