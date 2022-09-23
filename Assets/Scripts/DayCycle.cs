using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float speedAcceleration = 5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.x < 380f)
        {
            transform.Rotate(Vector2.right * Time.deltaTime * speedAcceleration);
        }
    }
}
