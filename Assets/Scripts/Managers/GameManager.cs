using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoSingleton<GameManager>
{
    DayManager DayManager;
    public bool IsEndGame => DayManager.Days == 2;
    public bool IsWinningState => ZombieSpawner.ZombiesInScene.Count == 0;

    private void Start()
    {
        Time.timeScale = 1;
        DayManager = FindObjectOfType<DayManager>();  
    }

    void Update()
    {
        if (IsEndGame && IsWinningState)
            HandleWin();
    }

    void HandleWin()
    {
        UIManager.Instance.EnableWinningPanel(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
