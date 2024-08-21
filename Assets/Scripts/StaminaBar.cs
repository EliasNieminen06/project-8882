using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] Slider staminaBar;

    // Update is called once per frame
    void Update()
    {
        staminaBar.value = Movement.instance.stamina / Movement.instance.maxStamina;
    }
}
