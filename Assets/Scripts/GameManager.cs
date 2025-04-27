using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameData GameData_SO;
        [SerializeField] private Brick[] Spawned_Bricks;

        private int _currentScore;
        private int _currentLife;

        private bool _isGamePaused;

       


        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            Instance = this;
        }



        private void Start()
        {
            GameEventManager.OnPlayerHit += UpdateHealth;
            GameEventManager.OnBallHitBrick += UpdateScore;
            //GameEventManager.OnGameOver += GameOver;
            LoadGameData();
        }



        private void OnDestroy()
        {
            GameEventManager.OnPlayerHit -= UpdateHealth;
            GameEventManager.OnBallHitBrick -= UpdateScore;
            //GameEventManager.OnGameOver -= GameOver;
        }

        public void PauseGame(InputAction.CallbackContext value)
        {
            GamePauseStatus(!_isGamePaused);
        }

        private void GamePauseStatus(bool satus)
        {
            _isGamePaused=satus;
            GameEventManager.TriggerGamePause(satus);
        }

        //also reset game
        private void LoadGameData()
        {
            _currentLife = GameData_SO.InitalLife;
            _currentScore = 0;
            BrickRandomizer();
            GameEventManager.TriggerScoreUpdate(_currentScore);
            GameEventManager.TriggleHealthUpdate(_currentLife);
        }

        private void UpdateHealth()
        {
            _currentLife--;
            if (_currentLife <= 0)
            {
                GameEventManager.TriggerGameOver();
            }
            _currentLife = Math.Clamp(_currentLife, 0, GameData_SO.InitalLife);
            GameEventManager.TriggleHealthUpdate(_currentLife);
            //reset the ball position

        }

        private void UpdateScore(int brickType)
        {
            _currentScore += GameData_SO.Brick_DataList[brickType].Points;
            GameEventManager.TriggerScoreUpdate(_currentScore);
        }

      

        public BrickData GetRandomBrickData()
        {
            return GameData_SO.Brick_DataList[Random.Range(0, GameData_SO.MaxBrickTypes)];
        }

        private void BrickRandomizer()
        {
            for (int i = 0; i < Spawned_Bricks.Length; i++)
            {
                Spawned_Bricks[i].SetBrickData(GetRandomBrickData());
            }
        }

        #region UI INPUTS

        public void Resumegame()
        {
            GamePauseStatus(true);
        }

        public void RestartGame()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        #endregion
    }
}