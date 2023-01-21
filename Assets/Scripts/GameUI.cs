using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI levelText;
    public GameObject endScreen;
    public GameObject pauseScreen;
    public GameObject infoScreen;
    public TextMeshProUGUI endScreenHeader;
    public TextMeshProUGUI endScreenScoreText;    

    public static GameUI instance;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateScoreText();
        UpdateHighScoreText();
        UpdateLevelText();
    }    

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + GameManager.instance.score;
    }

    public void UpdateHighScoreText()
    {   
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void UpdateLevelText()
    {     
        levelText.text = (SceneManager.GetActiveScene().buildIndex - 1).ToString();
    }

    public void SetEndScreen(bool hasWon)
    {
        endScreen.SetActive(true);        
        if (hasWon)
        {
            endScreenHeader.text = "You Win";
            endScreenHeader.color = Color.green;
        }
        else
        {
            endScreenHeader.text = "Game Over";
            endScreenHeader.color = Color.red;
        }

        endScreenScoreText.text = "<b>Score</b>\n" + GameManager.instance.score;
    }

    public void ActivateInfoScreen(bool showInfo)
    {
        infoScreen.SetActive(showInfo);
       // Cursor.lockState = CursorLockMode.None;
    }

    public void OnResumeButton()
    {
        GameManager.instance.TogglePauseGame();        
    }

    public void OnRestartButton()
    {
        GameManager.instance.score = 0;
        SceneManager.LoadScene(2);
        if (GameManager.instance.paused)
            GameManager.instance.TogglePauseGame();
        AmbienceController.instance.ChangeMusic("Ambience");
    }

    public void OnMenuButton()
    {
        GameManager.instance.score = 0;
        if (GameManager.instance.paused)
            GameManager.instance.TogglePauseGame();
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        AmbienceController.instance.ChangeMusic("Menu");
    }

    public void TogglePauseScreen(bool paused)
    {
        pauseScreen.SetActive(paused);
    }

    public void OnCloseButton()
    {        
        infoScreen.SetActive(false);
        Time.timeScale = 1f;
    }
}
