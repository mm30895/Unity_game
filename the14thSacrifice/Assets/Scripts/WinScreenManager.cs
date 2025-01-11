using UnityEngine;
using UnityEngine.SceneManagement;
public class WinScreenManager : MonoBehaviour
{
    public GameObject winPanel; // Reference to the win panel
    public GameObject playAgainButton; // Reference to the play again button
    public GameObject winBackground;

    public bool shown = false;

    private void Start()
    {
        shown = false;
        // Initially hide the win panel and play again button
        winPanel.SetActive(false);
        playAgainButton.SetActive(false); // Assuming you want to hide this initially too
        winBackground.SetActive(false);
    }

    public void show(){
        shown = true;
        winPanel.SetActive(true);
        playAgainButton.SetActive(true);
        winBackground.SetActive(true);
        Debug.Log("Win screen is shown. Unlocking cursor.");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
