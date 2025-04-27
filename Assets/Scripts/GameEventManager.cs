using System;
using System.Collections;
using UnityEngine;


public class GameEventManager : MonoBehaviour
{
    public static event Action<int> OnScoreUpdate;
    public static event Action<int> OnBallHitBrick;
    public static event Action<bool> OnGamePaused;

    public static event Action OnPlayerHit;
    public static event Action OnGameOver;

    public static event Action<int> OnHealthUpdate;

    public static void TriggerGameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void TriggerHealthHit()
    {
        OnPlayerHit?.Invoke();
    }

    public static void TriggleHealthUpdate(int updatedScore)
    {
        OnHealthUpdate?.Invoke(updatedScore);
    }

    public static void TriggerBallHit(int index)
    {
        OnBallHitBrick?.Invoke(index);
    }

    public static void TriggerScoreUpdate(int currentScore)
    {
        OnScoreUpdate?.Invoke(currentScore);
    }

    public static void TriggerGamePause(bool status)
    {
        OnGamePaused?.Invoke(status);
    }

    
}
