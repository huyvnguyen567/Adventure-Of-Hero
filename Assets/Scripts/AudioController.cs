using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    public AudioSource mainMenuMusic;
    public AudioSource levelMusic;
    public AudioSource bossMusic;
    public AudioSource[] playerSFX;
    public AudioSource[] enemyDeathSFX;
    public AudioSource[] uiSFX;
    public AudioSource[] effectSFX;
    
    void Awake()
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
    public void PlayMainMenuMusic()
    {
        mainMenuMusic.Play();
        levelMusic.Stop();
        bossMusic.Stop();
    }
    public void PlayLevelMusic()
    {
        mainMenuMusic.Stop();
        levelMusic.Play();
        bossMusic.Stop();
    }
    public void PlayBossMusic()
    {
        mainMenuMusic.Stop();
        levelMusic.Stop();
        bossMusic.Play();
    }
    public void PlayerSFX(int sfxPlayer)
    {
        playerSFX[sfxPlayer].Stop();
        playerSFX[sfxPlayer].Play();
    }
    public void EnemyDeathSFX(int sfxEnemy)
    {
        enemyDeathSFX[sfxEnemy].Stop();
        enemyDeathSFX[sfxEnemy].Play();
    }
    public void UiSFX(int sfxUi)
    {
        uiSFX[sfxUi].Stop();
        uiSFX[sfxUi].Play();
    }
    public void EffectSFX(int sfxEffect)
    {
        effectSFX[sfxEffect].Stop();
        effectSFX[sfxEffect].Play();
    }
}
