using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MonsterBar : MonoBehaviour
{
    public Image health_fill; 

    public void SetFill(float percentage)
    {
        health_fill.fillAmount = percentage;
    }
}
