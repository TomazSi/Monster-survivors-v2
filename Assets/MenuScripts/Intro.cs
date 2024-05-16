using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Check if the intro should be skipped
        bool skipIntro = PlayerPrefs.GetInt("skipIntro", 0) == 1;

        if (skipIntro)
        {
            // Skip to the main scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Press any key to continue
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
