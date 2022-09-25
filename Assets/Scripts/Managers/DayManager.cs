using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField, Range(0, 24)] float StartingTime = 7;
    [SerializeField, Range(0, 24)] private float CurrentTime;
    [SerializeField] private float DayDurationInSeconds = 300f;

    private int days = 1;
    public int Days { get { return days; } }

    float timeLapsed;
    float timePercent;

    void Start()
    {
        CurrentTime = StartingTime;
    }

    void Update()
    {
        if(days == 2)
        {
            Debug.Log("Hai vinto!");
            Time.timeScale = 0;
            return;
        }

        timeLapsed += Time.deltaTime;

        if(timeLapsed > DayDurationInSeconds)
        {
            days++;
            timeLapsed -= DayDurationInSeconds;
        }

        CurrentTime += Time.deltaTime / (DayDurationInSeconds / 24f);
        CurrentTime %= 24f;
        timePercent = CurrentTime / 24f;

        transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
    }
}
