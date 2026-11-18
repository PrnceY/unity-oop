using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool paused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Toggle();
    }

    public void Toggle()
    {
        paused = !paused;
        pausePanel.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f;
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
    }

        public void Resume() { Debug.Log("Resume called"); Toggle(); }

    public void GoToMenu() { Debug.Log("GoToMenu called"); Time.timeScale = 1f; SceneManager.LoadScene("MainMenu"); }
}