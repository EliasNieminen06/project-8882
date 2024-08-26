using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashLight;
    public bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        flashLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.flashlightBatteryLevel < 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isOn = !isOn;
                if (isOn)
                {
                    flashLight.SetActive(true);
                }
                else
                {
                    flashLight.SetActive(false);
                }
            }
        }
        else
        {
            flashLight.SetActive(false);
        }
    }
}
