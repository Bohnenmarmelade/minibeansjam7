using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    private EventManager _eventManager;

    private void Start()
    {
        _eventManager = GameObject.FindObjectOfType<EventManager>();
    }

    public void LoadGame()
    {
       _eventManager.OnGameStarted.Invoke();
    }
}
