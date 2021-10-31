using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceMusic;
    [SerializeField] private AudioSource _audioSourceSFX;

    [SerializeField] private Slider _sliderMusicVol;
    [SerializeField] private Slider _sliderSFXVol;

    [SerializeField] private AudioClip _runningClip;
    [SerializeField] private AudioClip _mouseClickClip;
    [SerializeField] private AudioClip _mouseHoverClip;
    [SerializeField] private AudioClip _levelSoundClip;
    [SerializeField] private AudioClip _successSound;

    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioClip _failSound;
    [SerializeField] private AudioClip _switchSound;
    [SerializeField] private AudioClip _buttonPress;
    [SerializeField] private AudioClip _goHomeAlarm;
    [SerializeField] private AudioClip _nearMeltdownAlarm;
    [SerializeField] private AudioClip _victoryWinScreenMusic;
    
    

    private bool running = false;

    private void Start()
    {
        _audioSourceMusic.volume = PlayerPrefs.GetFloat("musicvolume");
        _audioSourceSFX.volume = PlayerPrefs.GetFloat("sfxvolume");
        _sliderMusicVol.value = _audioSourceMusic.volume;
        _sliderSFXVol.value = _audioSourceSFX.volume;
    }

    private void Update()
    {
        while (running)
        {
            if (!_audioSourceSFX.isPlaying)
            {
                _audioSourceSFX.PlayOneShot(_runningClip);
            }
        }
    }

    public void SetSFXVol()
    {
        float vol = _sliderSFXVol.value;
        _audioSourceSFX.volume = vol;
        PlayerPrefs.SetFloat("sfxvolume",_audioSourceSFX.volume);
        PlayerPrefs.Save();
    }
    
    public void SetMusicVol()
    {
        float vol = _sliderMusicVol.value;
        _audioSourceMusic.volume = vol;
        PlayerPrefs.SetFloat("musicvolume",_audioSourceMusic.volume);
        PlayerPrefs.Save();
    }

    public void SetSliderVals()
    {
        _sliderSFXVol.value = _audioSourceSFX.volume;
        _sliderMusicVol.value =  _audioSourceMusic.volume;
        Debug.Log("");
    }

    public void PlayRunningSound()
    {
        if (!_audioSourceSFX.isPlaying)
        {
            _audioSourceSFX.PlayOneShot(_runningClip);
        }
    }
    public void PlayMouseClickSound()
    {
        _audioSourceSFX.PlayOneShot(_mouseClickClip);
        
    }
    public void PlayMouseHoverSound()
    {
        _audioSourceSFX.PlayOneShot(_mouseHoverClip);
    }

    public void PlayLevelPullSound()
    {
        _audioSourceSFX.PlayOneShot(_levelSoundClip); 
    } 

    public void PlaySuccessSound()
    {
        _audioSourceSFX.PlayOneShot(_successSound);
    }

    public void PlayGameOverSound()
    {
        _audioSourceMusic.Stop();
        _audioSourceSFX.Stop();
        _audioSourceSFX.PlayOneShot(_gameOverSound);
    }

    public void PlayFailSound()
    {
        _audioSourceSFX.PlayOneShot(_failSound);
    }
    public void PlaySwitchSound()
    {
        _audioSourceSFX.PlayOneShot(_switchSound);
    }
    public void PlayButtonSound()
    {
        _audioSourceSFX.PlayOneShot(_buttonPress);
    }

    public void PlayLevelOverAlarm()
    {
        if(!_audioSourceSFX.isPlaying)
            _audioSourceSFX.PlayOneShot(_goHomeAlarm);
    }

    public void PlayNearingMeltdownAlarm()
    {
        if(!_audioSourceSFX.isPlaying)
            _audioSourceSFX.PlayOneShot(_nearMeltdownAlarm);
    }
    public void PlayVictoryWinscreen()
    {
        _audioSourceMusic.Stop();
        _audioSourceSFX.Stop();
        _audioSourceSFX.PlayOneShot(_victoryWinScreenMusic);
    }
    
}
