using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGameManager : MonoBehaviour
{
    public Text deathMessage;
    public Text winMessage;
    public Image fadePanel;
    public float fadeDuration = 2f;

    public void PlayerDied()
    {
        Color currentColor = deathMessage.color;  // Get the current color of the Text component
        currentColor.a = 1f;  // Set the alpha value to 1 (fully opaque)
        deathMessage.color = currentColor;
        StartCoroutine(FadeToBlackAndLoadMenu());
    }

    public void PlayerWon()
    {
        // change alpha to 255
        Color currentColor = winMessage.color;  // Get the current color of the Text component
        currentColor.a = 1f;  // Set the alpha value to 1 (fully opaque)
        winMessage.color = currentColor;
        StartCoroutine(FadeToBlackAndLoadMenu());
    }

    IEnumerator FadeToBlackAndLoadMenu()
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        SceneManager.LoadScene("Menu");  // Load the Main Menu scene, make sure the scene name matches your scene settings
    }
}

