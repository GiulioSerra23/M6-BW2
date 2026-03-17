using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class UI_PauseMenu : MonoBehaviour
{
    [Header("Pause Panels")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject confirmRestartUI;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        confirmRestartUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Continue()
    {
        Resume();
    }

    public void QuitRun()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void OpenRestartConfirm()
    {
        confirmRestartUI.SetActive(true);
    }

    public void CloseRestartConfirm()
    {
        confirmRestartUI.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
}
