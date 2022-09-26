using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] DayManager DayManager;

    bool isPaused;

    PlayerStateMachine Player;

    public bool IsEndGame => DayManager?.Days == 2;
    public bool IsWinningState => ZombieSpawner.ZombiesInScene.Count == 0;


    private void OnEnable()
    {
        InputManager.Instance.OnPauseButton += PauseGame;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnPauseButton -= PauseGame;
    }

    private void Start()
    {
        Player = FindObjectOfType<PlayerStateMachine>();
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Player.IsDead)
            HandleDeath();

        if (IsEndGame && IsWinningState)
            HandleWin();
    }

    void HandleDeath()
    {
        UIManager.Instance.EnableDeathPanel(true);
    }

    void HandleWin()
    {
        UIManager.Instance.EnableWinningPanel(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
        isPaused = !isPaused;

        if(!isPaused)
        {
            Time.timeScale = 0;
            UIManager.Instance.EnablePausePanel(true);
        }
    }

    public void ResumeGame()
    {
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
