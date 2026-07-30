using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private string _gameSceneName = "Level01";

    public void OnPlayClicked()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void OnQuitClicked() 
    {
        Application.Quit();
        Debug.Log("Quit requested (only works in build, not Editor)");
    }
}
