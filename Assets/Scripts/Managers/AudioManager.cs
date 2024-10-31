using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager s_audioManager;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;
    private void Awake()
    {
        if (s_audioManager == null)
        {
            s_audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (s_audioManager != this)
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        EventsHandler.AudioEvents.OnPlayHoverAudio += PlayHoverSound;
        EventsHandler.AudioEvents.OnPlayBackgroundAudio += PlayBackgroundSound;
        EventsHandler.AudioEvents.OnPauseBackgroundAudio += PauseBackgroundSound;
        EventsHandler.AudioEvents.OnVolumeSliderChanged += SetVolume;
    }
    private void OnDestroy()
    {
        EventsHandler.AudioEvents.OnPlayHoverAudio -= PlayHoverSound;
        EventsHandler.AudioEvents.OnPlayBackgroundAudio -= PlayBackgroundSound;
        EventsHandler.AudioEvents.OnPauseBackgroundAudio -= PauseBackgroundSound;
        EventsHandler.AudioEvents.OnVolumeSliderChanged -= SetVolume;
    }
    private void Start()
    {
        _audioSource.clip = _audioClips[0];
    }
    private void PlayHoverSound()
    {
        _audioSource.PlayOneShot(_audioClips[1]);
    }
    private void PlayBackgroundSound()
    {
        _audioSource.Play();
    }
    private void PauseBackgroundSound()
    {
        _audioSource.Stop();
    }
    private void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }
    public float GetCurrentVolume()
    {
        return _audioSource.volume;
    }
}
