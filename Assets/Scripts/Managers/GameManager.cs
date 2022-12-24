using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] DayManager DayManager;

    PlayerStateMachine Player;

    public bool IsPause;
    
    bool isEndGame => DayManager.Days == 2;
    public bool IsEndGame { get { return isEndGame; } private set { } }
    bool isWinningState => ZombieSpawner.ZombiesInScene.Count == 0;

    private void Start()
    {
        Player = FindObjectOfType<PlayerStateMachine>();
    }

    void Update()
    {
        Time.timeScale = IsPause ? 0 : 1;

        if (IsPause)
            HandlePause();

        if (isEndGame && isWinningState)
            HandleWin();
    }

    void HandleDeath()
    {
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

    public void HandlePause()
    {
        UIManager.Instance.EnablePausePanel(IsPause);
    }
}
