using System;
using UnityEngine;

[Serializable]
public class GameData
{
    [SerializeField] private float playerMaxLife,
        playerCurrentLife,
        playerMaxMana,
        playerCurrentMana,
        playerDamage,
        fireballDamage,
        heavyDamage;

    public float PlayerCurrentLife
    {
        get => playerCurrentLife;
        set => playerCurrentLife = value;
    }

    public float PlayerCurrentMana
    {
        get => playerCurrentMana;
        set => playerCurrentMana = value;
    }

    public float PlayerMaxLife
    {
        get => playerMaxLife;
        set => playerMaxLife = value;
    }

    public float PlayerMaxMana
    {
        get => playerMaxMana;
        set => playerMaxMana = value;
    }

    public float PlayerDamage
    {
        get => playerDamage;
        set => playerDamage = value;
    }

    public float FireballDamage
    {
        get => fireballDamage;
        set => fireballDamage = value;
    }

    public float HeavyDamage
    {
        get => heavyDamage;
        set => heavyDamage = value;
    }
}