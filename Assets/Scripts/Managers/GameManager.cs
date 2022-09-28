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

    private void OnEnable()
    {
        InputManager.Instance.PauseEvent += OnPause;
    }

    private void OnDisable()
    {
        InputManager.Instance.PauseEvent -= OnPause;
    }

    void Update()
    {
        Time.timeScale = IsPause ? 0 : 1;

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
        UIManager.Instance.EnableWinningPanel(true);
        UIManager.Instance.EnablePausePanel(false);
    }

    public void OnPause()
    {
        IsPause = !IsPause;
        UIManager.Instance.EnablePausePanel(IsPause);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        IsPause = false;
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
