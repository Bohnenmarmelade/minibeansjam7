using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    private EventManager _eventManager;
    
    private float _loadGameAt = 0f;
    private bool _loadGame = false;

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
    }

    public void LoadGame()
    {
        _loadGame = true;
        _loadGameAt = Time.time + .2f;
    }
}
