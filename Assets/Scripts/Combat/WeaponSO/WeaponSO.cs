using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eWeaponType { Rifle, Pistol };

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons")]
public class WeaponSO : ScriptableObject
{
    public eWeaponType Type;
    public float FiringRate;
    public int Damage;
    public AudioClip FireSFX;
}
