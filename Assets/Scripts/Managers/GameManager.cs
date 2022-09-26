using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] DayManager DayManager;

    PlayerStateMachine Player;

    public bool IsPause;
    public bool IsEndGame => DayManager?.Days == 2;
    public bool IsWinningState => ZombieSpawner.ZombiesInScene.Count == 0;


    private void Start()
    {
        Player = FindObjectOfType<PlayerStateMachine>();
        Time.timeScale = 1;
    }

    void Update()
    {
        if(InputManager.Instance.IsPause)
        {
            HandlePause();
        }

        if(Player != null)
            if (Player.IsDead)
                HandleDeath();

        if (IsEndGame && IsWinningState)
            HandleWin();
    }

    void HandleDeath()
    {
        IsPause = true;
     
        UIManager.Instance.EnableDeathPanel(true);
    }

    void HandleWin()
    {
        IsPause = true;
        Time.timeScale = 0;

        UIManager.Instance.EnableWinningPanel(true);
    }

    public void HandlePause()
    {
        IsPause = true;
        Time.timeScale = 0;

        UIManager.Instance.EnablePausePanel(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        IsPause = false;

        Time.timeScale = 1;
        UIManager.Instance.EnablePausePanel(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
