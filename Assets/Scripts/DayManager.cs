using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField, Range(0, 24)] float StartingTime = 7;
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [SerializeField] private float DayDurationInSeconds = 300f;

    private int days = 1;
    public int Days { get { return days; } }

    float timeLapsed;

    void Start()
    {
        TimeOfDay = StartingTime;
    }

    void Update()
    {
        timeLapsed += Time.deltaTime;

        if(timeLapsed > DayDurationInSeconds)
        {
            days++;
            timeLapsed -= DayDurationInSeconds;
        }

        TimeOfDay += Time.deltaTime / (DayDurationInSeconds / 24f);
        TimeOfDay %= 24f;
        float timePercent = TimeOfDay / 24f;

        transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
    }
}
