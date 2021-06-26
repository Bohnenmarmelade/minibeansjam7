using Framework;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    public UnityEvent OnGameStarted { get; } = new UnityEvent();
    public UnityEvent OnLevelUp { get; } = new UnityEvent();
    public UnityEvent OnParalyzed { get; } = new UnityEvent();
    public UnityEvent OnGameOver { get; } = new UnityEvent();
    public UnityEvent OnAliceTouched { get; } = new UnityEvent();

    

}