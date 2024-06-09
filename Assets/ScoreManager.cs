using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }
        public int Score { get; private set; }

        public Text scoreText;

        private bool hasLoadedMenu = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(Instance);
            }
        }

        private void Update()
        {
            if (Score >= 1000 && !hasLoadedMenu)
            {
                hasLoadedMenu = true;
                FindObjectOfType<EndGameManager>().PlayerWon();
            }
        }

        public void AddScore(int points)
        {
            Score += points;
            if (scoreText == null)
            {
                Debug.LogError("ScoreManager: scoreText is not assigned!");
            }
            scoreText.text = "Score: " + Score;
        }
    }
}
