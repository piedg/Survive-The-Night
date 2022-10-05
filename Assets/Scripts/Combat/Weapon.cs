using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponSO Settings;

    public eWeaponType Type { get; private set; }
    public float FiringRate { get; private set; }
    public int Damage { get; private set; }
    public AudioClip FireSFX { get; private set; }

    private void Awake()
    {
        Type = Settings.Type;
        FiringRate = Settings.FiringRate;
        Damage = Settings.Damage;
        FireSFX = Settings.FireSFX;
    }
}
