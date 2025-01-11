using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject StartButton; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    
    public void OnStartButtonClick()
    {
        // Disable the start menu
        StartPanel.SetActive(false);
        StartButton.SetActive(false);
        // Start the game (this could load the first scene, enable gameplay elements, etc.)
        StartGame();
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
