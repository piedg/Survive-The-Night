using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] DayManager DayManager;

    PlayerStateMachine Player;

    bool IsInGame;
    public bool IsPause;
    public bool IsEndGame => DayManager?.Days == 2;
    public bool IsWinningState => ZombieSpawner.ZombiesInScene.Count == 0;

    private void Start()
    {
        IsInGame = false;

        Player = FindObjectOfType<PlayerStateMachine>();
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        if(IsInGame)
            InputManager.Instance.PauseEvent += OnPause;
    }

    private void OnDisable()
    {
        if (IsInGame)
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


}
