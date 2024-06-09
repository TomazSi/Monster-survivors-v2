using UnityEngine;
using UnityEngine.UI;

public class BarXP : MonoBehaviour
{
    public Slider BarXPFill;  // Assign this in the inspector
    public PlayerAttributes playerAttributes;  // Assign this in the inspector

    void Update()
    {
        UpdateXPBar();
    }

    private void UpdateXPBar()
    {
        if (playerAttributes != null)
        {
            float fillAmount = (float)playerAttributes.CurrentXP / playerAttributes.neededXP;
            BarXPFill.value = fillAmount;
        }
    }
}
