using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject carSelectionPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject gameOverPanel;

    public static UnityEvent finishEvent;
    public static UnityEvent gameOverEvent;

    private void Start()
    {
        Time.timeScale = 1;

        if(finishPanel != null)
            finishPanel.SetActive(false);

        if (finishEvent == null)
            finishEvent = new UnityEvent();

        finishEvent.AddListener(Finish);

        if (gameOverEvent == null)
            gameOverEvent = new UnityEvent();

        gameOverEvent.AddListener(GameOver);
    }

    public void StopButton()
    {
        CarController.isBreaking = true;
    }

    public void NoStopButton()
    {
        CarController.isBreaking = false;
    }

    public void Restart()
    {
        StartCoroutine(ResetTime());
    }

    IEnumerator ResetTime()
    {
        CarController.isRestart = true;
        yield return new WaitForSeconds(0.1f);
        CarController.isRestart = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayButton()
    {
        mainMenuPanel.SetActive(false);
        carSelectionPanel.SetActive(true);
    }

    public void BackButton()
    {
        mainMenuPanel.SetActive(true);
        carSelectionPanel.SetActive(false);
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        MyCarSound.soundOffEvent.Invoke();

        pausePanel.SetActive(true);
    }

    public void ContinueButton()
    {
        pausePanel.SetActive(false);

        MyCarSound.soundOnEvent.Invoke();
        Time.timeScale = 1;
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Finish()
    {
        Time.timeScale = 0;
        MyCarSound.soundOffEvent.Invoke();

        finishPanel.SetActive(true);
    }

    public void GameOver()
    {
        MyCarSound.soundOffEvent.Invoke();
        Invoke("GameOverDelay", 2);
    }

    public void GameOverDelay()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
