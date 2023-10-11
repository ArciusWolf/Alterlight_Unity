using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatContainer", menuName = "ScriptableObjects/Stat")]
public class StatContainer : ScriptableObject
{
    [Header("Character Stats")]
    public int Health;
    public int Stamina;
    public int Damage;
    public int Defense;
    public int Speed;
    public int critRate;
    public int critDamage;
}
