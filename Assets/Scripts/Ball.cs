using Assets.Scripts;
using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Ball_Rigidbody2D;
    [SerializeField] private float Speed;

    [SerializeField] private Vector2 StartingPosition;
    private Vector2 _velocityBeforepause;

    private void Start()
    {
        //GameEventManager.OnPlayerHit += ResetBall;
        GameEventManager.OnGamePaused += StopBall;
        GameEventManager.OnGameOver += GameOverState;
        
        StartBallMove();
    }



    private void OnDestroy()
    {
        //GameEventManager.OnPlayerHit -= ResetBall;
        GameEventManager.OnGamePaused -= StopBall;
        GameEventManager.OnGameOver -= GameOverState;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.TryGetComponent<Brick>(out Brick brick))
        {
            other.gameObject.SetActive(false);
            GameEventManager.TriggerBallHit(brick.BrickType);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            ResetBall();
            GameEventManager.TriggerHealthHit();
        }
    }

    public void ResetBall()
    {
        transform.position = StartingPosition;
        StartBallMove();
    }

    public void GameOverState()
    {
        StopBall();
    }

    public void StartBallMove(Vector2? velocity = null)
    {
        Vector2 moveVelocity = velocity ?? Vector2.up;
        Ball_Rigidbody2D.linearVelocity = moveVelocity * Speed;
    }

    private void StopBall(bool isPaused=false)
    {
        if (isPaused)
            _velocityBeforepause = Ball_Rigidbody2D.linearVelocity;

        Ball_Rigidbody2D.linearVelocity = isPaused ? Vector2.zero : _velocityBeforepause;
    }
}
