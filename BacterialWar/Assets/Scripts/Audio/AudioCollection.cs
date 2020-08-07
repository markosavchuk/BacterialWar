using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCollection : SingletonMonoBehaviour<AudioCollection>
{
    private static float _backgroundMusicStopTime = 0;

    protected override void OnAwake()
    {
        base.OnAwake();

        BackgroundMusic.time = _backgroundMusicStopTime;
        BackgroundMusic.Play();
    }

    [SerializeField]
    public AudioSource BackgroundMusic;

    [SerializeField]
    public AudioSource BuildSound;

    [SerializeField]
    public AudioSource ClickSound;

    [SerializeField]
    public AudioSource PointAttackSound;

    [SerializeField]
    public AudioSource FreezeSound;

    [SerializeField]
    public AudioSource WaveAttackSound;

    [SerializeField]
    public AudioSource InfectionSound;

    [SerializeField]
    public AudioSource VictoryMusic;

    public void StopBackgroundMusic()
    {
        BackgroundMusic.Stop();
        _backgroundMusicStopTime = 0;
    }

    public void SaveBackgroundMusicTime()
    {
        _backgroundMusicStopTime = BackgroundMusic.time;
    }
}
