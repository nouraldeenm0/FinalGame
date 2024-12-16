using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using TMPro;           

public class HUDManager : MonoBehaviour
{
    public Slider healthBar;
    public Slider xpBar;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI xpText;

    public void UpdateHealth(float current, float max)
    {
        healthBar.value = current / max;
        healthText.text = $"{current} / {max}";
    }

    public void UpdateXP(float current, float max)
    {
        xpBar.value = current / max;
        xpText.text = $"{current} / {max}";
    }
}
