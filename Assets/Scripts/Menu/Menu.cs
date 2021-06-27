using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    private EventManager _eventManager;
    
    private float _loadGameAt = 0f;
    private bool _loadGame = false;
    private float _loadTutorialAt = 0f;
    private bool _loadTutorial = false;

    private void Start()
    {
        _eventManager = GameObject.FindObjectOfType<EventManager>();
    }

    private void Update()
    {
        if (_loadGame && _loadGameAt < Time.time)
        {
            _loadGame = false;
            _eventManager.OnGameStarted.Invoke();
        }

        if (_loadTutorial && _loadTutorialAt < Time.time)
        {
            _loadTutorial = false;
            _eventManager.OnTutorialStarted.Invoke();
        }
    }

    public void LoadTutorial()
    {
        
        _loadTutorial = true;
        _loadTutorialAt = Time.time + .2f;
    }

    public void LoadGame()
    {
        _loadGame = true;
        _loadGameAt = Time.time + .2f;
    }
}
