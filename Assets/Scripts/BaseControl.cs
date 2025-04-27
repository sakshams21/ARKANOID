using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseControl : MonoBehaviour
{
    [SerializeField] private float MinValue;
    [SerializeField] private float MaxValue;
    private Camera _camera;

    private bool _platformStatus=true;

    private void Start()
    {
        _camera = Camera.main;

        GameEventManager.OnGameOver += GameOver;
        GameEventManager.OnGamePaused += GamePause;

    }

    private void OnDestroy()
    {
        GameEventManager.OnGameOver -= GameOver;
        GameEventManager.OnGamePaused -= GamePause;
    }

    private void GamePause(bool status)
    {
        _platformStatus = !status;
    }

    private void GameOver()
    {
        _platformStatus = false;
    }


    public void PlatformMove(InputAction.CallbackContext value)
    {
        if (!_platformStatus) return;

        float worldValue = _camera.ScreenToWorldPoint(value.ReadValue<Vector2>()).x;
        worldValue = Math.Clamp(worldValue, MinValue, MaxValue);
        Vector3 newBasePosition = new Vector3(worldValue, transform.position.y, 0);
        transform.position = newBasePosition;
    }
}
