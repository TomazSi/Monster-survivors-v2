using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
    public class ScoreManager : MonoBehaviour
    {
        public int Score { get; private set; }

        public Text scoreText;

        private bool hasLoadedMenu = false;

        private void Update()
        {
            if (Score >= 20 && !hasLoadedMenu)
            {
                hasLoadedMenu = true;
                StartCoroutine(LoadSceneAsync());
            }
        }

        IEnumerator LoadSceneAsync()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");
            asyncLoad.allowSceneActivation = false;  // Initially prevent the scene from activating

            while (!asyncLoad.isDone)
            {
                if (asyncLoad.progress >= 0.9f)  // Unity loads up to 90% and waits for activation
                {
                    asyncLoad.allowSceneActivation = true;  // Activate the scene
                }
                yield return null;
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
