using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private EventManager _eventManager;
    private void Start()
    {
        SceneManager.LoadScene(1);
        _eventManager = GameObject.FindObjectOfType<EventManager>();
        _eventManager.OnGameStarted.AddListener(OnGameStart);
        _eventManager.OnGameOver.AddListener(OnGameLost);
    }

    private void OnDestroy()
    {
        _eventManager.OnGameStarted.RemoveListener(OnGameStart);
        _eventManager.OnGameOver.RemoveListener(OnGameLost);
    }

    public void OnGameStart()
    {
        SceneManager.LoadScene(2);
    }

    public void OnGameLost()
    {
        SceneManager.LoadScene(3);
    }
}
