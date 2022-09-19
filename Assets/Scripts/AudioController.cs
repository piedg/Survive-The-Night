using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource[] Sources;

    public static AudioController Instance = null;

    void Awake()
    {
        Instance = Instance == null ? this : null;

        Sources = GetComponents<AudioSource>();
    }

    public void PlayClip(AudioClip _clip)
    {
        for (int i = 0; i < Sources.Length; i++)
        {
                Sources[i].clip = _clip;
                Sources[i].Play();
                break;
        }
    }
}
