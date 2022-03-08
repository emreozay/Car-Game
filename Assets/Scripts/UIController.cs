using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject carSelectionPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TextMeshProUGUI timeText;

    private Button soundOnButton;
    private Button soundOffButton;
    private Button closePausePanelButton;

    public static UnityEvent finishEvent;
    public static UnityEvent gameOverEvent;

    private bool isGameOver = false;

    private float timeLeft = 5f;

    private void Start()
    {
        Time.timeScale = 1;
        isGameOver = false;

        timeLeft = 5 * SceneManager.GetActiveScene().buildIndex + 15;

        if(finishPanel != null)
            finishPanel.SetActive(false);

        if (finishEvent == null)
            finishEvent = new UnityEvent();

        finishEvent.AddListener(Finish);

        if (gameOverEvent == null)
            gameOverEvent = new UnityEvent();

        gameOverEvent.AddListener(GameOver);

        if(pausePanel != null)
        {
            soundOnButton = pausePanel.transform.GetChild(0).transform.Find("SoundOnButton").GetComponent<Button>();
            soundOffButton = pausePanel.transform.GetChild(0).transform.Find("SoundOffButton").GetComponent<Button>();

            closePausePanelButton = pausePanel.transform.GetChild(0).transform.Find("CloseButton").GetComponent<Button>();
        }

        if(soundOnButton != null)
            soundOnButton.onClick.AddListener(SoundOn);

        if(soundOffButton != null)
            soundOffButton.onClick.AddListener(SoundOff);

        if (closePausePanelButton != null)
            closePausePanelButton.onClick.AddListener(ContinueButton);

        if(soundOnButton != null && soundOffButton != null)
        {
            if (PlayerPrefs.GetInt("Sound", 1) == 1)
            {
                soundOnButton.interactable = false;
                soundOffButton.interactable = true;
            }
            else
            {
                soundOnButton.interactable = true;
                soundOffButton.interactable = false;
            }
        }
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if(timeText != null)
        {
            timeText.text = timeLeft.ToString("0.00");
            if (timeLeft <= 0f && !isGameOver)
                GameOver();
        }
    }

    private void SoundOn()
    {
        PlayerPrefs.SetInt("Sound", 1);
        
        soundOnButton.interactable = false;
        soundOffButton.interactable = true;
        //MyCarSound.soundOnEvent.Invoke();
    }

    private void SoundOff()
    {
        PlayerPrefs.SetInt("Sound", 0);

        soundOnButton.interactable = true;
        soundOffButton.interactable = false;
        //MyCarSound.soundOffEvent.Invoke();
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
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 1));
    }

    public void PlayButton()
    {
        carSelectionPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void BackButton()
    {
        mainMenuPanel.SetActive(true);
        carSelectionPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void ShopButton()
    {
        shopPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void PauseButton()
    {
        MyCarSound.soundOffEvent.Invoke();
        Time.timeScale = 0;

        pausePanel.SetActive(true);
    }

    public void ContinueButton()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
            MyCarSound.soundOnEvent.Invoke();
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Finish()
    {
        Time.timeScale = 0;
        MyCarSound.soundOffEvent.Invoke();

        if(PlayerPrefs.GetInt("Level", 0) != 30)
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);

        finishPanel.SetActive(true);
    }

    public void GameOver()
    {
        MyCarSound.soundOffEvent.Invoke();

        isGameOver = true;

        if (timeLeft > 0)
            Invoke("GameOverDelay", 1.5f);
        else
            GameOverDelay();
    }

    public void GameOverDelay()
    {
        Time.timeScale = 0;
        print("BURADA 1");
        PlayerPrefs.SetInt("AdCounter", PlayerPrefs.GetInt("AdCounter", 0) + 1);
        if (PlayerPrefs.GetInt("AdCounter", 0) >= 3)
        {
            if(InterstitialAd.interstitialAdEvent != null)
            {
                InterstitialAd.interstitialAdEvent.Invoke();
                PlayerPrefs.SetInt("AdCounter", 0);
            }
        }
        print("BURADA 2");

        gameOverPanel.SetActive(true);
        print("BURADA 3");

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
