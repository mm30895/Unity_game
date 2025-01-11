using UnityEngine;
using UnityEngine.SceneManagement;
public class WinScreenManager : MonoBehaviour
{
    public GameObject winScreen;


    public bool shown = false;

    private void Start()
    {
        shown = false;
        // Initially hide the win panel and play again button
        winScreen.SetActive(false);
    }

    public void show(){
        shown = true;
        winScreen.SetActive(true);
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
