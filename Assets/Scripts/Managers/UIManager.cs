using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public Image DamagePanelBackground;
    public GameObject WinningPanel;
    public Button RestartGameBtn;
    public Button BackMainMenuBtn;

    Color color;


    private void Start()
    {
        color = DamagePanelBackground.color;
        RestartGameBtn.onClick.AddListener(() => GameManager.Instance.RestartGame());
        BackMainMenuBtn.onClick.AddListener(() => GameManager.Instance.BackMainMenu());
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
