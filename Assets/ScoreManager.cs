using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }
        public int Score { get; private set; }

        public Text scoreText;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
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
