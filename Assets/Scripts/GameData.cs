using System;
using UnityEngine;

[Serializable]
public class GameData
{
    [SerializeField] private float playerMaxLife,
        playerMaxJumps,
        playerCurrentLife,
        playerMaxMana,
        playerCurrentMana,
        playerDamage,
        fireballDamage,
        heavyDamage;

    [SerializeField] private int sceneIndex;
    [SerializeField] private bool boss1Defeated, hasDash;

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

    public float PlayerMaxJumps
    {
        get => playerMaxJumps;
        set => playerMaxJumps = value;
    }

    public int SceneIndex
    {
        get => sceneIndex;
        set => sceneIndex = value;
    }

    public bool Boss1Defeated
    {
        get => boss1Defeated;
        set => boss1Defeated = value;
    }

    public bool HasDash
    {
        get => hasDash;
        set => hasDash = value;
    }
}