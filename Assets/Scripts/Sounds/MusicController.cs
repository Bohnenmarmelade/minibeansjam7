using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip levelMusic;
    public AudioClip gameOverMusic;
    
    private AudioClip _clipToPlay;
    
    private AudioSource _source;
    
    private bool _fadeIn = false;
    private bool _fadeOut = false;

    private float _fadeTime = .2f;
    private float _startVolume;

    private EventManager _eventManager;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();

        _eventManager = GameObject.FindObjectOfType<EventManager>();

        _clipToPlay = menuMusic;
        _startVolume = _source.volume;
        _source.volume = 0.0f;
        _source.clip = _clipToPlay;
        _source.Play();
        _fadeIn = true;
    }
    private void Update () {
        if (_fadeOut) {
            _source.volume -= _startVolume * Time.deltaTime / _fadeTime;
            if (_source.volume < 0.1) {
                _fadeOut = false;
                _fadeIn = true;
                _source.clip = _clipToPlay;
                _source.Play();
            }
        } else if (_fadeIn) {
            _source.volume += _startVolume * Time.deltaTime / _fadeTime;
            if (_source.volume > _startVolume) {
                _fadeIn = false;
            }
        }
    }

    private void OnEnable()
    {
        _eventManager.OnGameStarted.AddListener(HandleLevelMusic);
        _eventManager.OnGameOver.AddListener(HandleGameOverMusic);
    }

    private void OnDisable()
    {
        _eventManager.OnGameStarted.RemoveListener(HandleLevelMusic);
        _eventManager.OnGameOver.RemoveListener(HandleGameOverMusic);
    }

    private void HandleGameOverMusic()
    {
        _clipToPlay = gameOverMusic;
        _fadeOut = true;
    }

    private void HandleMenuMusic()
    {
        _clipToPlay = menuMusic;
        _fadeOut = true;
    }

    private void HandleLevelMusic()
    {
        _clipToPlay = levelMusic;
        _fadeOut = true;
    }
        
}