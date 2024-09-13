using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI percentagetext;

    // Update is called once per frame
    void Update()
    {
        int percentage = (int)GameManager.instance.flashlightBatteryLevel;
        percentagetext.text = percentage.ToString() + "%";
    }
}
