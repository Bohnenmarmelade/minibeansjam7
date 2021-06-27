using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public UnityEvent OnGameStarted { get; } = new UnityEvent();
    public UnityEvent OnTutorialStarted { get; } = new UnityEvent();
    
    public UnityEvent OnLevelUp { get; } = new UnityEvent();
    public UnityEvent OnParalyzed { get; } = new UnityEvent();
    public UnityEvent OnGameOver { get; } = new UnityEvent();
    public UnityEvent OnAliceTouched { get; } = new UnityEvent();

    

}