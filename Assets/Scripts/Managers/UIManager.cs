using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public Image DamagePanelBackground;
    
    [Header("PausePanel")]
    public GameObject PausePanel;
    public Button BackMainMenuOnPauseBtn;
    public Button ResumeOnPauseBtn;
    public Button SettingsOnPauseBtn;

    [Header("WinningPanel")]
    public GameObject WinningPanel;
    public Button RestartGameOnWinBtn;
    public Button BackMainMenuOnWinBtn;

    public GameObject DeathPanel;


    Color color;

    private void Start()
    {
        color = DamagePanelBackground.color;

        RestartGameOnWinBtn.onClick.AddListener(() => GameManager.Instance.RestartGame());
        BackMainMenuOnWinBtn.onClick.AddListener(() => GameManager.Instance.BackMainMenu());
        ResumeOnPauseBtn.onClick.AddListener(() => GameManager.Instance.ResumeGame());
        BackMainMenuOnPauseBtn.onClick.AddListener(() => GameManager.Instance.BackMainMenu());
    }

    public void EnableDeathPanel(bool enable)
    {
        DeathPanel.SetActive(enable);
    }

    public void EnablePausePanel(bool enable)
    {
        PausePanel.SetActive(enable);
    }

    public void EnableWinningPanel(bool enable)
    {
        WinningPanel.SetActive(enable);
    }
   
    public void SetDamageBgColor()
    {
        color.a = 0.15f;
        DamagePanelBackground.color += color;
        
        FadeMe();
    }

    public void FadeMe()
    {
        StartCoroutine(DoFade());
    }

    IEnumerator DoFade()
    {
        while (DamagePanelBackground.color.a > 0)
        {
            color.a -= Time.deltaTime / 2;
            DamagePanelBackground.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }
}
