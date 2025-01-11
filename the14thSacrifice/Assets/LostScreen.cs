using UnityEngine;
using UnityEngine.SceneManagement;

public class LostScreen : MonoBehaviour
{
    public GameObject LostPanel;
    public GameObject LostButton;
    
    void Start(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LostPanel.SetActive(true);
        LostButton.SetActive(true);
    }

    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
