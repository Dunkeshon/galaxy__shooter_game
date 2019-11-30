using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool GameOver = true;
    public GameObject player;
    private UI_manager _ui_manager;
    public GameObject pauseMenu;
    [SerializeField]
    private GameObject _phone_controls;
    public bool gameispaused=false;
    private player _playerscript;
    public void Start()
    {
        _ui_manager = GameObject.Find("Canvas").GetComponent<UI_manager>();
       _playerscript = player.GetComponent<player>();
    }
     
    private void Update()
    {
        if (GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player);
                    GameOver = false;
                    _ui_manager.HideTitleScreen();
                }
        }
    }
    public void PausingTheGame()
    {
        Debug.Log("must hide");
       // _phone_controls.SetActive(false);
        gameispaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
       
      //  Debug.Log("must be already hiden");

      //  _playerscript._need_to_find = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        StartCoroutine(PauseSequenceRoutine());
        gameispaused = false;
      
    }
    public void Returntomenu()
    {
        AudioListener.pause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameOver = true;
    }
    public void Restart()
    {
        AudioListener.pause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameOver = true;
        _playerscript._need_to_find = true;// tut check if it'll work with public
    }
    public IEnumerator PauseSequenceRoutine()
    {
        yield return new WaitForSecondsRealtime (1f); 
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}
