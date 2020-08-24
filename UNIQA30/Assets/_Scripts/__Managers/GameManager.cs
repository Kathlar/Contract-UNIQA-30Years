using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public static PlayerController player { get; private set; }

    public GameObject loseCanvas;
    public Text losePointsText;
    public GameObject pointsCanvas;
    public Text pointsText;
    int points;
    public Text countdownText;

    protected override void AwakeSingleton()
    {
        base.AwakeSingleton();
        player = FindObjectOfType<PlayerController>(); 
        Time.timeScale = 1;
    }

    private void Start()
    {
        loseCanvas.SetActive(false);

        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        countdownText.text = "";

        yield return new WaitForSeconds(1);
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "";

        player.StartGame();
        FollowTransformLerp lerp = FindObjectOfType<FollowTransformLerp>();
        if (lerp) lerp.DoStart();
    }

    public static void Lose()
    {
        Instance.pointsCanvas.SetActive(false);
        Instance.loseCanvas.SetActive(true);
        Instance.losePointsText.text = Instance.points.ToString();
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void UpdatePoints(int points)
    {
        Instance.pointsText.text = points.ToString();
        Instance.points = points;
    }

    public static void SaveScore(int points)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int currentScore = PlayerPrefs.GetInt(sceneName + "Points");
        if(points > currentScore)
        {
            PlayerPrefs.SetString(sceneName + "Nick", PlayerPrefs.GetString("PlayerNick"));
            PlayerPrefs.SetInt(sceneName + "Points", points);
            PlayerPrefs.Save();
        }
    }
}
