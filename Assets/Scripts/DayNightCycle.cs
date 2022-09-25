using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float speedAcceleration = 5f;
    [SerializeField, Range(0, 24)] private float TimeOfDay; 
    // Start is called before the first frame update
    void Start()
    {
        TimeOfDay = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        TimeOfDay += Time.deltaTime / 12.5f;
        TimeOfDay %= 24;

        float timePercent = TimeOfDay / 24f;

        transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
    }
}
