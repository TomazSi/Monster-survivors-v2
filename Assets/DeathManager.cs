using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathManager : MonoBehaviour
{
    public Text deathMessage;
    public Image fadePanel;
    public float fadeDuration = 2f;

    public void PlayerDied()
    {
        deathMessage.enabled = true;  // Show the "You Died" message
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

