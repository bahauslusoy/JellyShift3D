using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //----------------------------------------------
    //Thank you for purchasing the asset! If you have any questions/suggestions, don't hesitate to contact me!
    //E-mail: ragendom@gmail.com
    //Please let me know your impressions about the asset by leaving a review, I will appreciate it.
    //----------------------------------------------

    public GameObject startPanel, endPanel, skinsPanel, pausedPanel, pauseButton, muteImage, reviveButton;
    public TextMeshProUGUI scoreText, highScoreText, endScoreText, endHighScoreText;
    public Animation cameraAnim;

    [HideInInspector]
    public bool gameIsOver = false, gameHasStarted = false;

	void Start () {
        //UNCOMMENT THE FOLLOWING LINES IF YOU ENABLED UNITY ADS AT UNITY SERVICES AND REOPENED THE PROJECT!
        //if (FindObjectOfType<AdManager>().unityAds)
        //    CallUnityAds();     //Calls Unity Ads
        //else
        CallAdmobAds();     //Calls Admob Ads

        StartPanelActivation();
        HighScoreCheck();
        AudioCheck();
        CheckCamera();
	}

    //UNCOMMENT THE FOLLOWING LINES IF YOU ENABLED UNITY ADS AT UNITY SERVICES AND REOPENED THE PROJECT!
    //public void CallUnityAds()
    //{
    //    if (Time.time != Time.timeSinceLevelLoad)
    //        FindObjectOfType<AdManager>().ShowUnityVideoAd();      //Shows Interstitial Ad when game starts (except for the first time)
    //    FindObjectOfType<AdManager>().HideAdmobBanner();
    //}

    public void CallAdmobAds()
    {
        FindObjectOfType<AdManager>().ShowAdmobBanner();        //Shows Banner Ad when game starts
        if (Time.time != Time.timeSinceLevelLoad)
            FindObjectOfType<AdManager>().ShowAdmobInterstitial();      //Shows Interstitial Ad when game starts (except for the first time)
    }

    public void Initialize()
    {
        scoreText.enabled = false;
        pauseButton.SetActive(false);
        FindObjectOfType<PlayerScale>().enabled = false;
    }

    public void StartPanelActivation()
    {
        Initialize();
        startPanel.SetActive(true);
        skinsPanel.SetActive(false);
        endPanel.SetActive(false);
        pausedPanel.SetActive(false);
    }

    public void SkinsPanelActivation()
    {
        startPanel.SetActive(false);
        skinsPanel.SetActive(true);
        pausedPanel.SetActive(false);
    }

    public void SkinsBackButton()
    {
        StartPanelActivation();
        FindObjectOfType<AudioManager>().ButtonClickSound();
    }

    public void EndPanelActivation()
    {
        StopObstacles();

        gameIsOver = true;
        FindObjectOfType<AudioManager>().DeathSound();
        FindObjectOfType<PlayerScale>().enabled = false;
        FindObjectOfType<Spawner>().CancelInvoke();
        startPanel.SetActive(false);
        endPanel.SetActive(true);
        pausedPanel.SetActive(false);
        skinsPanel.SetActive(false);
        scoreText.enabled = false;
        endScoreText.text = scoreText.text;
        pauseButton.SetActive(false);
        HighScoreCheck();
    }

    public void StopObstacles()
    {
        foreach (GameObject obst in GameObject.FindGameObjectsWithTag("ObstacleHolder"))
            obst.GetComponent<ObstacleHolder>().StopObstacle();
    }

    public void MoveObstacles()
    {
        foreach (GameObject obst in GameObject.FindGameObjectsWithTag("ObstacleHolder"))
            obst.GetComponent<ObstacleHolder>().MoveObstacle();
    }

    public void PausedPanelActivation()
    {
        startPanel.SetActive(false);
        endPanel.SetActive(false);
        pausedPanel.SetActive(true);
    }

    public void HighScoreCheck()
    {
        if (FindObjectOfType<ScoreManager>().score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", FindObjectOfType<ScoreManager>().score);
        }
        highScoreText.text = "BEST " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        endHighScoreText.text = "BEST " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void AudioCheck()
    {
        if (PlayerPrefs.GetInt("Audio", 0) == 0)
        {
            muteImage.SetActive(false);
            FindObjectOfType<AudioManager>().soundIsOn = true;
            FindObjectOfType<AudioManager>().PlayBackgroundMusic();
        }
        else
        {
            muteImage.SetActive(true);
            FindObjectOfType<AudioManager>().soundIsOn = false;
            FindObjectOfType<AudioManager>().StopBackgroundMusic();
        }
    }

    public void StartButton()
    {
        gameHasStarted = true;
        pauseButton.SetActive(true);
        scoreText.enabled = true;
        startPanel.SetActive(false);
        FindObjectOfType<AudioManager>().ButtonClickSound();
        FindObjectOfType<PlayerScale>().enabled = true;
        FindObjectOfType<Spawner>().Spawn();
    }

    public void RestartButton()
    {
        FindObjectOfType<AudioManager>().ButtonClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AudioButton()
    {
        FindObjectOfType<AudioManager>().ButtonClickSound();
        if (PlayerPrefs.GetInt("Audio", 0) == 0)
            PlayerPrefs.SetInt("Audio", 1);
        else
            PlayerPrefs.SetInt("Audio", 0);
        AudioCheck();
    }

    public void PauseButton()
    {
        pauseButton.SetActive(false);
        PausedPanelActivation();
        scoreText.enabled = false;
        FindObjectOfType<AudioManager>().StopBackgroundMusic();
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().PlayBackgroundMusic();
        scoreText.enabled = true;
        pauseButton.SetActive(true);
        pausedPanel.SetActive(false);
    }

    public void HomeButton()
    {
        ResumeButton();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Revive()
    {
        //UNCOMMENT THE FOLLOWING LINES IF YOU ENABLED UNITY ADS AT UNITY SERVICES AND REOPENED THE PROJECT!
        //if (FindObjectOfType<AdManager>().unityAds)
        //    FindObjectOfType<AdManager>().ShowUnityRewardVideoAd();       //Shows Unity Reward Video ad
        //else
        FindObjectOfType<AdManager>().ShowAdmobRewardVideo();       //Shows Admob Reward Video ad

        gameIsOver = false;

        endPanel.SetActive(false);
        reviveButton.SetActive(false);
        pauseButton.SetActive(true);
        scoreText.enabled = true;
        FindObjectOfType<PlayerScale>().enabled = true;
        MoveObstacles();
        FindObjectOfType<Spawner>().Invoke("Spawn", 1f);
        FindObjectOfType<Collision>().Invoke("CanCollideAgain", 0.5f);
    }

    public void CameraButton()
    {
        if(PlayerPrefs.GetInt("Camera", 0) == 0)
        {
            cameraAnim.Play("MiddleViewAnim");
            PlayerPrefs.SetInt("Camera", 1);
        }
        else
        {
            cameraAnim.Play("SideViewAnim");
            PlayerPrefs.SetInt("Camera", 0);
        }
    }

    public void CheckCamera()
    {
        if (PlayerPrefs.GetInt("Camera", 0) == 0)
            cameraAnim.Play("SideViewAnim");
        else
            cameraAnim.Play("MiddleViewAnim");
    }
}
