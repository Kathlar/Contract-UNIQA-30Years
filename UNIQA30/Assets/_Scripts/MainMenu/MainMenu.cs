using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    enum MenuPart { Regular, LevelSelect, HighScore }
    MenuPart currentMenuPart = MenuPart.Regular;

    public GameObject mainMenu, levelSelectMenu, highScoresMenu;
    public InputField nickText;
    public Text highScoreText;
    public Dropdown sexDropdown;

    private static GameObject Music;
    public GameObject music;

    private void Start()
    {
        nickText.SetTextWithoutNotify(PlayerPrefs.GetString("PlayerNick"));
        string currentSex = PlayerPrefs.GetString("PlayerSex");
        if (currentSex == "Man") sexDropdown.SetValueWithoutNotify(1);
        else if (currentSex == "Woman") sexDropdown.SetValueWithoutNotify(0);
        if (Music) Destroy(music);
        else Music = music;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    public void PlayButton()
    {
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
        currentMenuPart = MenuPart.LevelSelect;
    }

    public void HighScoreButton()
    {
        mainMenu.SetActive(false);
        highScoresMenu.SetActive(true);
        currentMenuPart = MenuPart.HighScore;

        string hst = "";
        for(int i = 0; i < 4; i++)
        {
            hst += "Level " + (i + 1).ToString() + ". ";

            string nick = PlayerPrefs.GetString("Level" + (i+1).ToString() + "Nick");
            int points = PlayerPrefs.GetInt("Level" + (i + 1).ToString() + "Points");

            if (nick != null && points != 0)
            {
                hst += nick + " " + points.ToString();
            }
            else hst += "-";

            hst += "\n";
        }
        highScoreText.text = hst;
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        levelSelectMenu.SetActive(false);
        highScoresMenu.SetActive(false);
        currentMenuPart = MenuPart.Regular;
    }

    public void LoadLevel(int number)
    {
        string levelName = "Level" + number.ToString();
        SceneManager.LoadScene(levelName);
    }

    public void UniqaButton()
    {
        Application.OpenURL("https://www.uniqa.pl/");
    }

    public void OnNickSet()
    {
        PlayerPrefs.SetString("PlayerNick", nickText.text);
        PlayerPrefs.Save();
    }

    public void OnSexSet()
    {
        PlayerPrefs.SetString("PlayerSex", sexDropdown.value == 0 ? "Woman" : "Man");
        PlayerPrefs.Save();
    }
}
