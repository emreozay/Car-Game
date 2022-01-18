using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject carSelectionPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI timeText;

    public static UnityEvent finishEvent;
    public static UnityEvent gameOverEvent;

    private float timeLeft = 5f;

    private void Start()
    {
        Time.timeScale = 1;

        timeLeft = 10 * SceneManager.GetActiveScene().buildIndex;

        if(finishPanel != null)
            finishPanel.SetActive(false);

        if (finishEvent == null)
            finishEvent = new UnityEvent();

        finishEvent.AddListener(Finish);

        if (gameOverEvent == null)
            gameOverEvent = new UnityEvent();

        gameOverEvent.AddListener(GameOver);
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeText != null)
        {
            timeText.text = timeLeft.ToString("0.00");
            if (timeLeft <= 0f)
                GameOver();
        }
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

        if (timeLeft > 0)
            Invoke("GameOverDelay", 1.5f);
        else
            GameOverDelay();
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
