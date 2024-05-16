using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Toggle skipIntroToggle;

    // Start is called before the first frame update
    void Start()
    {
        skipIntroToggle = GetComponent<Toggle>();

        bool skipIntro = PlayerPrefs.GetInt("skipIntro", 0) == 1;
        skipIntroToggle.isOn = skipIntro;

        skipIntroToggle.onValueChanged.AddListener(onSkipIntroToggleChanged);
    }

    private void onSkipIntroToggleChanged(bool value)
    {
        PlayerPrefs.SetInt("skipIntro", value ? 1 : 0);
    }
}
