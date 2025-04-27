using System;
using System.Collections;
using TMPro;
using UnityEngine;


namespace Assets.Scripts
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Health_Text;
        [SerializeField] private TextMeshProUGUI Score_Text;
        [SerializeField] private Canvas PauseGameOverScreen_Canvas;
        [SerializeField] private GameObject PauseScreen_Go;
        [SerializeField] private GameObject GameOverScreen_Go;
        
        void Start()
        {
            GameEventManager.OnHealthUpdate+= UpdateHealth;
            GameEventManager.OnScoreUpdate += UpdateScore;
            GameEventManager.OnGamePaused += GamePauseScreen;
            GameEventManager.OnGameOver += GameOverScreen;
        }

       

        private void OnDestroy()
        {
            GameEventManager.OnHealthUpdate -= UpdateHealth;
            GameEventManager.OnScoreUpdate -= UpdateScore;
            GameEventManager.OnGamePaused -= GamePauseScreen;
            GameEventManager.OnGameOver -= GameOverScreen;
        }

        private void UpdateHealth(int currentHealth)
        {
            Health_Text.text = $"Lives: {currentHealth}";
            
        }
        private void UpdateScore(int currentScore)
        {
            Score_Text.text = $"Score: {currentScore}";
        }

        private void GamePauseScreen(bool status)
        {
            PauseGameOverScreen_Canvas.enabled = status;
            PauseScreen_Go.SetActive(status);
        }  
        
        private void GameOverScreen()
        {
            PauseGameOverScreen_Canvas.enabled = true;
            GameOverScreen_Go.SetActive(true);
        }
    }
}