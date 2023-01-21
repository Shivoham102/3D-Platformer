using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene(2);
        AmbienceController.instance.ChangeMusic("Ambience");
    }

    public void OnLevelsButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnResetButton()
    {
        PlayerPrefs.DeleteAll();
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
