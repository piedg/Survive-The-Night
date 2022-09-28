using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] DayManager DayManager;

    PlayerStateMachine Player;

    public bool IsPause;
    bool isEndGame => DayManager.Days == 2;
    public bool IsEndGame { get; private set; }
    bool isWinningState => ZombieSpawner.ZombiesInScene.Count == 0;

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

        if (Player.IsDead)
                HandleDeath();

        if (isEndGame && isWinningState)
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

    public void ResumeGame()
    {
        IsPause = false;
        UIManager.Instance.EnablePausePanel(false);
    }

    public void OnPause()
    {
        Debug.Log("Sono qui");
        IsPause = !IsPause;
        UIManager.Instance.EnablePausePanel(IsPause);
      
    }

}
