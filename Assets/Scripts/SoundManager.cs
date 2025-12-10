using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] AudioClipRefsSO soundRefsSO;
    private float volume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayerController.Instance.AttackedSword += PlayerOnAttackedSword;
        PlayerController.Instance.SwordHit += PlayerOnSwordHit;
        PlayerController.Instance.TookDamage += PlayerOnTookDamage;
    }

    private void PlayerOnTookDamage(object sender, EventArgs e)
    {
        PlaySound(soundRefsSO.playerHit, PlayerController.Instance.transform.position);
    }

    public void PlayRuneSound()
    {
        PlaySound(soundRefsSO.runeSound, PlayerController.Instance.transform.position);
    }

    private void PlayerOnSwordHit(object sender, EventArgs e)
    {
        PlaySound(soundRefsSO.swordHit, PlayerController.Instance.transform.position);
    }

    private void PlayerOnAttackedSword(object sender, EventArgs e)
    {
        Debug.Log("PlayerOnAttackedSword");
        PlaySound(soundRefsSO.attack, PlayerController.Instance.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }
}