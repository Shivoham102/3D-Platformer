using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public static GameManager instance;
    public bool paused;    
    public int[] levelStatus = { 1, 0, 0, 0, 0, 0 };
    PlayerData data;
    public bool showInfoUI = true;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        data = new PlayerData();
    }

    private void Start()
    {
        GameUI.instance.UpdateHighScoreText();       
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1 && Input.GetButtonDown("Cancel"))
        {
            TogglePauseGame();
        }

        if (SceneManager.GetActiveScene().buildIndex == 5 && showInfoUI)
        {
            showInfoUI = false;
            GameUI.instance.ActivateInfoScreen(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            AmbienceController.instance.ChangeMusic("LavaAmbience");
        }
    }

    public void TogglePauseGame()
    {
        if (!GameUI.instance.endScreen.activeSelf)
        {
            paused = !paused;

            if (paused)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }

            GameUI.instance.TogglePauseScreen(paused);
        }
        
    }

    public void AddScore(int scoreToGive)
    {
        score += scoreToGive;
        GameUI.instance.UpdateScoreText();
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {        
            PlayerPrefs.SetInt("HighScore", score);
            GameUI.instance.UpdateHighScoreText();
        }
    }

    public void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            GameUI.instance.UpdateHighScoreText();
        }            
    }

    public void LevelEnd()
    {
        if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
        {
            levelStatus[SceneManager.GetActiveScene().buildIndex - 2] = 1;
            data.levelStatus = levelStatus;
            string statusString = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("LevelStatus", statusString);
            GameWin();
        }
        else
        {           
            levelStatus[SceneManager.GetActiveScene().buildIndex - 2] = 1;
            data.levelStatus = levelStatus;
            string statusString = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("LevelStatus",statusString);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                AmbienceController.instance.ChangeMusic("NightAmbience");
            }
        }
    }

    public void GameWin()
    {
        AmbienceController.instance.ChangeMusic("GameWin");
        GameUI.instance.SetEndScreen(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;    
    }

    public void GameOver()
    {
        AmbienceController.instance.ChangeMusic("GameOver");
        GameUI.instance.SetEndScreen(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;      
    }
}
