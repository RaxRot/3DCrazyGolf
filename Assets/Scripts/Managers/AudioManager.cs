using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private AudioSource[] levelTracks;

    [Header("SFX")]
    [SerializeField] private AudioSource[] sfx;

    private bool _levelMusicPlaying;
    private int _currentTrack;
    
    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        ControlMusic();
    }

    private void ControlMusic()
    {
        if (_levelMusicPlaying)
        {
            if (!levelTracks[_currentTrack].isPlaying)
            {
                _currentTrack++;
                if (_currentTrack>=levelTracks.Length)
                {
                    _currentTrack = 0;
                }
                levelTracks[_currentTrack].Play();
            }
        }
    }

    public void PlayMenuMusic()
    {
        menuMusic.Play();

        _levelMusicPlaying = false;
        levelTracks[_currentTrack].Stop();
    }

    public void PlayLevelMusic()
    {
        menuMusic.Stop();
        _levelMusicPlaying = true;
        if (!levelTracks[_currentTrack].isPlaying)
        {
            levelTracks[_currentTrack].Play();
        }
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }
}
