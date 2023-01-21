using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelMenuUI : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonParent;
    private int[] levelStatusDefault = { 1, 0, 0, 0, 0, 0 };
    private string levelStatus;

    private void Awake()
    {
        levelStatus = PlayerPrefs.GetString("LevelStatus");            
    }

    private void OnEnable()
    {                 
        for(int i = 0; i < SceneManager.sceneCountInBuildSettings - 2; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonParent.transform);
            int levelNumber = i + 1;          
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + (i + 1).ToString();      
            if (levelStatus.Length == 0)
            {
                if (levelStatusDefault[levelNumber - 1] == 0)
                    newButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                PlayerData data = JsonUtility.FromJson<PlayerData>(levelStatus);          
                if (data.levelStatus[levelNumber - 1] == 0)
                    newButton.GetComponent<Button>().interactable = false;
            }

            newButton.GetComponent<Button>().onClick.AddListener(() => SelectLevel(levelNumber + 1, levelNumber > 3 ? "LavaAmbience" : "Ambience"));
        }
    }

    private void SelectLevel(int levelNumber, string ambienceType)
    {
        SceneManager.LoadScene(levelNumber);
        AmbienceController.instance.ChangeMusic(ambienceType);
    }

    public void onBackButton()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
