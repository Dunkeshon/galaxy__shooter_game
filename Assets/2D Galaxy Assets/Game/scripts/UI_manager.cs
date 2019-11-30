using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_manager : MonoBehaviour
{
    public Sprite[] lives;
    public Image player_lives;
    public Text scoreText;
    public Text HighScoreText;
    public int score=0;
    public int HighScore = 0;
    public GameObject titleScreen;
    public Text Flashing_text;
    private float Timer=0;
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _playerLives;
    [SerializeField]
    private GameObject _SCORE;
    [SerializeField]
    private GameObject _pauseButton;
    [SerializeField]
    private GameObject _HIGHSCORE;
    [SerializeField]
    private GameObject _phone_controls;
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        HighScore = PlayerPrefs.GetInt("HighScore",0);
        HighScoreText.text = "High Score: " + HighScore;
    }
    private void Update()
    {
        if (_gameManager.GameOver)
        {
            Flashing_text_in_menu();
            _playerLives.SetActive(false);
            _SCORE.SetActive(false);
            _pauseButton.SetActive(false);
            _HIGHSCORE.SetActive(false);
            _phone_controls.SetActive(false);
        }
        else
        {
            Flashing_text.enabled = false;
            _playerLives.SetActive(true);
            _SCORE.SetActive(true);
            _pauseButton.SetActive(true);
            _HIGHSCORE.SetActive(true);
            if (_gameManager.gameispaused)
            {
                _phone_controls.SetActive(false);
            }
            else
            {
                _phone_controls.SetActive(true);
            }
        }
    }
    public void Updatelives(int CurrentLives)
    {
        if (player_lives != null)
        {
            player_lives.sprite = lives[CurrentLives];
        }
    }
    public void UpdateScore(int objectScore)
    {
        score += objectScore;
        scoreText.text = "Score : " +  score;
    }
    public void UpdateHighScore()
    {
        if (score>HighScore)
        {
            HighScore = score;
            PlayerPrefs.SetInt("HighScore", HighScore);
            HighScoreText.text = "High Score : " + HighScore;
        }
    }
    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
    }
   public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
        score = 0;
        UpdateScore(0);  
    }

    public void Flashing_text_in_menu()
    {
        Timer += Time.deltaTime;
        if (Timer >= 0.7f)
        {
            Flashing_text.enabled = true;
        }
        if (Timer >= 1.4F)
        {
            Flashing_text.enabled = false;
            Timer = 0;
        }
    }

    public void Return_to_main_menu()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.Returntomenu();
        SceneManager.LoadScene("Main_Menu");
    }
    public void Restart()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject player = GameObject.FindWithTag("Player");
        gm.Restart();
        ShowTitleScreen();
        Destroy(player);



    }
}
