using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 120f;

    void Update()
    {
        transform.Rotate(0f, RotationSpeed * Time.deltaTime, 0f);
    }
}
