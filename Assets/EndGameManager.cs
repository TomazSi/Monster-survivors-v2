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

    private void Start()
    {
        // Initialize messages to be invisible at start
        SetAlpha(deathMessage, 0f);
        SetAlpha(winMessage, 0f);
        SetAlpha(fadePanel, 0f);
    }

    public void PlayerDied()
    {
        SetAlpha(deathMessage, 1f);
        StartCoroutine(FadeToBlackAndLoadMenu());
    }

    public void PlayerWon()
    {
        SetAlpha(winMessage, 1f);
        StartCoroutine(FadeToBlackAndLoadMenu());
    }

    private void SetAlpha(Graphic graphic, float alpha)
    {
        if (graphic != null)
        {
            Color currentColor = graphic.color;
            currentColor.a = alpha;
            graphic.color = currentColor;
        }
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
